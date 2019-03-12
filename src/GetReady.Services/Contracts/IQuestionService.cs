namespace GetReady.Services.Contracts
{
    using GetReady.Services.Models.QuestionModels;

    public interface IQuestionService
    {
        int CreatePersonal(QuestionCreate data, int userId);

        int CreateGlobal(QuestionCreate data);


        void DeleteGlobal(int id);

        void DeletePersonal(int id, int userId);

        void DeleteAllPersonalForSheet(int id, int userId);


        QuestionGlobalGet GetGlobal(int id);

        QuestionPersonalGet GetPersonal(int id, int userId);


        void EditPersonal(QuestionEdit data, int userId);

        void EditGlobal(QuestionEdit data);


        void CopyQuestions(CopyQuestions data, int userId);

        void AddNewScore(int score, int questionId, int userId);

        void Reorder(ReorderData data, int userId);

        void ReorderGlobal(ReorderData data);

        void SuggestForPublishing(int personalQuestionId, int userId); 
    }
}
