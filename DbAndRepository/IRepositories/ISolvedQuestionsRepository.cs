using DbAndRepository.GenericsEFRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAndRepository.IRepositories
{
    public interface ISolvedQuestionsRepository : IGenericsRepository<SolvedQuestion>
    {
        List<SolvedQuestion> GetByWorker(int id);

        List<SolvedQuestion> FindSimilarQuestions(string question, string[] keywords, string topic);
        List<int> GetLastSevenDaysSolvedQuestions(out List<DateTime> d, out List<KeyValuePair<string, int>> byName);


    }
}
