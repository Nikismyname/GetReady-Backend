#region INIT
namespace GetReady.Services.Implementations
{
    using AutoMapper;
    using GetReady.Data;
    using GetReady.Data.Models.QuestionModels;
    using GetReady.Services.Contracts;
    using GetReady.Services.Exceptions;
    using GetReady.Services.Models.QuestionModels;
    using GetReady.Services.Utilities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class QuestionService : IQuestionService
    {
        private readonly GetReadyDbContext context;

        public QuestionService(GetReadyDbContext context)
        {
            this.context = context;
        }
        #endregion

        #region Create 
        public int CreateGlobal(QuestionCreate data)
        {
            var parentSheet = this.context.QuestionSheets
                .SingleOrDefault(x => x.Id == data.SheetId && x.IsGlobal == true);

            if (parentSheet == null)
            {
                throw new ServiceException("Invalid Parent Sheet!");
            }

            var result = new GlobalQuestionPackage
            {
                Name = data.Name,
                Question = data.Question,
                Answer = data.Answer,
                Comment = data.Comment,
                Difficulty = data.Difficulty.Value,
                QuestionSheetId = data.SheetId,
            };

            context.GlobalQuestionPackages.Add(result);
            context.SaveChanges();
            return result.Id;
        }

        public int CreatePersonal(QuestionCreate data, int userId)
        {
            var parentSheet = context.QuestionSheets.SingleOrDefault(x => x.Id == data.SheetId);
            if (parentSheet == null)
            {
                throw new ServiceException("Invalid Parent Sheet!");
            }

            if (parentSheet.IsGlobal == true)
            {
                throw new ServiceException("Invalid Parent Sheet!");
            }

            if (parentSheet.UserId != userId)
            {
                throw new ServiceException("Parent Sheet does not belong to you!");
            }

            var user = context.Users.SingleOrDefault(x => x.Id == userId);
            if (user == null)
            {
                throw new ServiceException("Invalid User!");
            }

            var result = new PersonalQuestionPackage
            {
                Name = data.Name,
                Question = data.Question,
                Answer = data.Answer,
                Comment = data.Comment,
                Difficulty = data.Difficulty.Value,
                QuestionSheetId = data.SheetId,

                TimesBeingAnswered = 0,
                YourBestAnswer = "Good try buddy!",
                AnswerRate = 0,
            };

            context.PersonalQuestionPackages.Add(result);
            context.SaveChanges();
            return result.Id;
        }
        #endregion

        #region Delete
        public void DeleteGlobal(int id)
        {
            var questionToDelete = context.GlobalQuestionPackages.SingleOrDefault(x => x.Id == id);
            if (questionToDelete == null)
            {
                throw new ServiceException("Question to delete does not exist!");
            }

            context.GlobalQuestionPackages.Remove(questionToDelete);
            context.SaveChanges();
        }

        public void DeletePersonal(int id, int userId)
        {
            var user = this.context.Users.SingleOrDefault(x => x.Id == userId);
            if (user == null)
            {
                throw new ServiceException(Constants.UserNotFoundMessage);
            }

            var queston = this.context.PersonalQuestionPackages.SingleOrDefault(x => x.Id == id);
            if (queston == null)
            {
                throw new ServiceException(Constants.PersonalQuestionNotFoundMessage);
            }

            var parentSheetUserId = this.context.QuestionSheets
                .SingleOrDefault(x => x.Id == queston.QuestionSheetId && x.IsGlobal == false)?
                .UserId;

            if (parentSheetUserId == null)
            {
                throw new ServiceException(Constants.ParentSheetIsInvalid);
            }

            if (parentSheetUserId != user.Id)
            {
                throw new ServiceException(Constants.ParentSheetDoesNotBelongToYou);
            }

            this.context.PersonalQuestionPackages.Remove(queston);
            this.context.SaveChanges();
        }


        public void DeleteAllPersonalForSheet(int id, int userId)
        {
            var user = this.context.Users.SingleOrDefault(x => x.Id == userId);
            if (user == null)
            {
                throw new ServiceException(Constants.UserNotFoundMessage);
            }

            var sheet = this.context.QuestionSheets
                .Include(x => x.PersonalQuestions)
                .SingleOrDefault(x => x.Id == id && x.IsGlobal == false);
            if (sheet == null)
            {
                throw new ServiceException("Sheet Not Found!");
            }

            if (sheet.UserId != user.Id)
            {
                throw new ServiceException("Sheet does not beling to you!");
            }

            context.PersonalQuestionPackages.RemoveRange(sheet.PersonalQuestions);
            context.SaveChanges();
        }
        #endregion

        #region Get
        public QuestionGlobalGet GetGlobal(int id)
        {
            var model = this.context.GlobalQuestionPackages.SingleOrDefault(x => x.Id == id);
            if (model == null)
            {
                throw new ServiceException(Constants.WantedQuestionDoesNotExist);
            }

            var result = Mapper.Map<QuestionGlobalGet>(model);
            return result;
        }

        public QuestionPersonalGet GetPersonal(int id, int userId)
        {
            var user = context.Users.SingleOrDefault(x => x.Id == userId);
            if (user == null)
            {
                throw new ServiceException(Constants.UserNotFoundMessage);
            }

            var personalQuestion = this.context.PersonalQuestionPackages
                .SingleOrDefault(x => x.Id == id);
            if (personalQuestion == null)
            {
                throw new ServiceException(Constants.WantedQuestionDoesNotExist);
            }

            var parentSheet = this.context.QuestionSheets
                .SingleOrDefault(x => x.Id == personalQuestion.QuestionSheetId);
            if (parentSheet == null || parentSheet.UserId != userId)
            {
                throw new ServiceException("This Question Does Not Belong To You!");
            }

            var result = Mapper.Map<QuestionPersonalGet>(personalQuestion);
            return result;
        }
        #endregion

        #region Edit
        public void EditPersonal(QuestionEdit data, int userId)
        {
            var user = this.context.Users.SingleOrDefault(x => x.Id == userId);
            if (user == null)
            {
                throw new ServiceException(Constants.UserNotFoundMessage);
            }

            var question = this.context.PersonalQuestionPackages
                .SingleOrDefault(x => x.Id == data.Id);
            if (question == null)
            {
                throw new ServiceException("Question Not Found!");
            }

            var questionSheetUserId = this.context.QuestionSheets
                .SingleOrDefault(x => x.Id == question.QuestionSheetId)?.UserId;
            if (questionSheetUserId == null)
            {
                throw new ServiceException("Invalid Parent Question Sheet!");
            }

            if (questionSheetUserId != user.Id)
            {
                throw new ServiceException("Question Dos Not Belong To You!");
            }

            question.Name = data.Name;
            question.Question = data.Question;
            question.Comment = data.Comment;
            question.Answer = data.Answer;
            question.Difficulty = data.Difficulty.Value;
            context.SaveChanges();
        }

        public void EditGlobal(QuestionEdit data)
        {
            var question = this.context.GlobalQuestionPackages
                .SingleOrDefault(x => x.Id == data.Id);
            if (question == null)
            {
                throw new ServiceException("Question Not Found!");
            }

            question.Name = data.Name;
            question.Question = data.Question;
            question.Comment = data.Comment;
            question.Answer = data.Answer;
            question.Difficulty = data.Difficulty.Value;
            context.SaveChanges();
        }
        #endregion

        #region Other
        public void CopyQuestions(CopyQuestions data, int userId)
        {
            var user = this.context.Users.SingleOrDefault(x => x.Id == userId);
            if (user == null)
            {
                throw new ServiceException(Constants.UserNotFoundMessage);
            }

            var targetDirectory = this.context.QuestionSheets
                .SingleOrDefault(x => x.Id == data.SelectedDir && x.IsGlobal == false);

            if (targetDirectory == null)
            {
                throw new ServiceException("Target Directory Does Not Exist!");
            }

            if (targetDirectory.UserId != user.Id)
            {
                throw new ServiceException("Target Directory Does Not Belong To You!");
            }

            var globalQuestions = this.context.GlobalQuestionPackages
                .Where(x => data.SelectedQuestions.Contains(x.Id))
                .ToArray();

            var personalQuestions = new List<PersonalQuestionPackage>();
            for (int i = 0; i < globalQuestions.Length; i++)
            {
                var x = globalQuestions[i];
                personalQuestions.Add(new PersonalQuestionPackage
                {
                    Name = x.Name,
                    Question = x.Question,
                    Answer = x.Answer,
                    Comment = x.Comment,
                    Difficulty = x.Difficulty,
                    AnswerRate = 0,
                    TimesBeingAnswered = 0,
                    YourBestAnswer = "This is the first time you are answering this question",
                    QuestionSheetId = targetDirectory.Id,
                });
            }

            this.context.PersonalQuestionPackages.AddRange(personalQuestions);
            context.SaveChanges();
        }

        const int numberOfStoredScores = 20;

        public void AddNewScore(int score, int questionId, int userId)
        {
            if (score < 1 || score > 10)
            {
                throw new ServiceException("Score must be between 1 and 10!");
            }

            var user = context.Users.SingleOrDefault(x => x.Id == userId);
            if (user == null)
            {
                throw new ServiceException("User not found!");
            }

            var question = context.PersonalQuestionPackages.SingleOrDefault(x => x.Id == questionId);
            if (question == null)
            {
                throw new ServiceException("Question not found!");
            }

            var parentSheetUserId = context.QuestionSheets.SingleOrDefault(x => x.Id == question.QuestionSheetId)?.UserId;
            if (parentSheetUserId == null)
            {
                throw new ServiceException("Parent Sheet Not Found!");
            }

            if (parentSheetUserId != user.Id)
            {
                throw new ServiceException("Question does not beling to you!");
            }

            var scoresString = question.LastTenScores;
            if (scoresString != null)
            {
                var scores = scoresString.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
                if (scores.Count == 0)
                {
                    question.LastTenScores = score.ToString();
                }
                else
                {
                    if (scores.Count >= numberOfStoredScores)
                    {
                        scores.RemoveAt(0);
                    }
                    scores.Add(score.ToString());
                    question.LastTenScores = string.Join(",", scores);
                }
            }
            else
            {
                question.LastTenScores = score.ToString();
            }

            context.SaveChanges();
        }

        public void Reorder(ReorderData data, int userId)
        {
            var sheet = this.context.QuestionSheets
                .SingleOrDefault(x => x.Id == data.SheetId && x.IsGlobal == false);
            if (sheet == null)
            {
                throw new ServiceException("Sheet to Reorder Does not Exist!");
            }

            var user = this.context.Users
                .SingleOrDefault(x => x.Id == userId);
            if (user == null)
            {
                throw new ServiceException(Constants.UserNotFoundMessage);
            }

            if (user.Id != sheet.UserId)
            {
                throw new ServiceException("Sheet Does Not Belong To You!");
            }

            var questions = this.context.PersonalQuestionPackages
                .Where(x => x.QuestionSheetId == sheet.Id)
                .ToArray();

            var sentIds = data.Orderings.Select(x => x[0]).ToArray();

            if (sentIds.Length != sentIds.Distinct().ToArray().Length)
            {
                throw new ServiceException("Invalid Reorder Data!");
            }

            if (questions.Length != sentIds.Length)
            {
                throw new ServiceException("Invalid Reorder Data!");
            }

            if (questions.Select(x => x.Id).Any(x => !sentIds.Contains(x)))
            {
                throw new Exception("Invalid Reorder Data!");
            }

            for (int i = 0; i < questions.Length; i++)
            {
                var question = questions[i];
                var ordering = data.Orderings.Single(x => x[0] == question.Id);
                var order = ordering[1];
                var col = ordering[2];

                question.Order = order;
                question.Column = col;
            }

            context.SaveChanges();
        }

        public void ReorderGlobal(ReorderData data)
        {
            var sheet = this.context.QuestionSheets
                .SingleOrDefault(x => x.Id == data.SheetId && x.IsGlobal == true);
            if (sheet == null)
            {
                throw new ServiceException("Sheet to Reorder Does not Exist!");
            }

            var questions = this.context.GlobalQuestionPackages
                .Where(x => x.QuestionSheetId == sheet.Id)
                .ToArray();

            var sentIds = data.Orderings.Select(x => x[0]).ToArray();

            if (sentIds.Length != sentIds.Distinct().ToArray().Length)
            {
                throw new ServiceException("Invalid Reorder Data!");
            }

            if (questions.Length != sentIds.Length)
            {
                throw new ServiceException("Invalid Reorder Data!");
            }

            if (questions.Select(x => x.Id).Any(x => !sentIds.Contains(x)))
            {
                throw new Exception("Invalid Reorder Data!");
            }

            for (int i = 0; i < questions.Length; i++)
            {
                var question = questions[i];
                var ordering = data.Orderings.Single(x => x[0] == question.Id);
                var order = ordering[1];
                var col = ordering[2];

                question.Order = order;
                question.Column = col;
            }

            context.SaveChanges();
        }

        public void SuggestForPublishing(int personalQuestionId, int userId)
        {
            var user = context.Users
                .SingleOrDefault(x => x.Id == userId);
            if(user == null)
            {
                throw new ServiceException("User Not Found!");
            }

            var question = context.PersonalQuestionPackages
                .SingleOrDefault(x => x.Id == personalQuestionId);
            if(question == null)
            {
                throw new ServiceException("Question Not Found!");
            }

            var sheetUserId = context.QuestionSheets
                .SingleOrDefault(x => x.Id == question.QuestionSheetId)?.UserId;
            if(sheetUserId == null)
            {
                throw new ServiceException("Parent Sheet Not Found!");
            }

            if(sheetUserId != user.Id)
            {
                throw new ServiceException("Question Does not belong to you!");
            }

            var proposedQuestion = Mapper.Map<GlobalQuestionPackage>(question);
            proposedQuestion.Approved = false;
            proposedQuestion.QuestionSheetId = -1;
            proposedQuestion.Order = 0;
            proposedQuestion.Column = 0;

            context.GlobalQuestionPackages.Add(proposedQuestion);
            context.SaveChanges();
        }
    }
    #endregion
}
