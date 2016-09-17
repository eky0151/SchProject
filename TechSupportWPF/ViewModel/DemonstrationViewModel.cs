namespace SchProject.ViewModel
{
    using GalaSoft.MvvmLight;
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using Microsoft.Practices.ServiceLocation;

    public class RegistratdUsers : ObservableObject
    {
        private int count;
        public int Count
        {
            get { return count; }
            set { Set(ref count, value); }
        }

        private string time;
        public string Time
        {
            get { return time; }
            set { Set(ref time, value); }
        }
    }
    public class SolvedQuestionsByDay : ObservableObject
    {
        private DateTime  time;
        public DateTime Time
        {
            get { return time; }
            set { Set(ref time, value); }
        }

        private int count;
        public int Count
        {
            get { return count; }
            set { Set(ref count, value); }
        }
    }

    public class DemonstrationViewModel : ViewModelBase
    {
        public ObservableCollection<SolvedQuestionsByDay> Data { get; private set; }
            = new ObservableCollection<SolvedQuestionsByDay>();

        public ObservableCollection<RegistratdUsers> Users { get; private set; }
            = new ObservableCollection<RegistratdUsers>();

        private int[] Counts;
        private DateTime[] Dates;

        private KeyValuePair<string, int>[] pie;
        public KeyValuePair<string, int>[] Pie
        {
            get { return pie; }
        }


        public  DemonstrationViewModel()
        {
            Init();
        }

        private  void Init()
        {
            Counts = ServiceLocator.Current.GetInstance<TechSupportServer>().host.GetLastSevedDaysSolves(out Dates, out pie);

            for (int i = 0; i < Counts.Length; i++)
            {
                Data.Add(new SolvedQuestionsByDay
                {
                    Time = Dates[i],
                    Count = Counts[i]
                });
            }

            Counts = ServiceLocator.Current.GetInstance<TechSupportServer>().host.GetLastMonthRegistratedUsers(out Dates);

            for (int i = 0; i < Counts.Length; i++)
            {
                Users.Add(new RegistratdUsers
                {
                    Count = Counts[i],
                    Time = Dates[i].ToShortDateString()
                });
            }
        }
    }
}
