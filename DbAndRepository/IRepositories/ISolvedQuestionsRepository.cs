namespace DbAndRepository.IRepositories
{
    using DbAndRepository.GenericsEFRepository;
    using System;
    using System.Collections.Generic;

    public interface ISolvedQuestionsRepository : IGenericsRepositoryNoDUM<SolvedQuestion>
    {
        List<SolvedQuestion> GetByWorker(int id);

        List<SolvedQuestion> FindSimilarQuestions(string question, string[] keywords, string topic);
        List<int> GetLastSevenDaysSolvedQuestions(out List<DateTime> d, out List<KeyValuePair<string, int>> byName);


    }
}
