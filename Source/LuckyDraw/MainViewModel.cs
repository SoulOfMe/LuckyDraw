using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace LuckyDraw
{
    /// <summary>
    /// 主页面的ViewModel
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            //LoadData();
            TestData();
        }

        /// <summary>
        /// 剩余奖品数
        /// </summary>
        public int PrizeCount
        {
            get
            {
                return _prizesList.Count;
            }
        }

        /// <summary>
        /// 剩余待抽奖人数
        /// </summary>
        public int PeopleCount
        {
            get
            {
                return _peopleList.Count;
            }
        }

        #region 界面列表数据
        private ObservableCollection<Prize> _prizesList = new ObservableCollection<Prize>();
        private ObservableCollection<Person> _peopleList = new ObservableCollection<Person>();

        /// <summary>
        /// 剩余奖品列表
        /// </summary>
        public ObservableCollection<Prize> PrizesList
        {
            get
            {
                return _prizesList;
            }
        }

        /// <summary>
        /// 剩余抽奖人数列表
        /// </summary>
        public ObservableCollection<Person> PeopleList
        {
            get
            {
                return _peopleList;
            }
        }
        #endregion

        private void LoadData()
        {
            //todo:读取奖品，抽奖人，中奖列表三个csv文件，更新数据列表
        }

        private void TestData()
        {
            //测试数据
            _prizesList.Clear();
            _peopleList.Clear();
            for (int i = 1; i <= 100; i++)
            {
                _prizesList.Add(new Prize()
                {
                    ID = i,
                    Name = "Prize_" + i.ToString(),
                    IsUsed = false
                });
            }

            for (int i = 1; i <= 1000; i++)
            {
                _peopleList.Add(new Person()
                {
                    ID = 1,
                    Name = "Persion_" + i.ToString(),
                    IsUsed = false
                });
            }
        }

        public void RefrehList()
        {
            //去除已经抽过的.并写入CSV文件
            List<Prize> prizes = new List<Prize>();
            List<Person> persons = new List<Person>();

            foreach (var i in _prizesList)
            {
                if (i.IsUsed)
                {
                    prizes.Add(i);
                }
            }
            foreach (var i in _peopleList)
            {
                if (i.IsUsed)
                {
                    persons.Add(i);
                }
            }

            foreach (var i in prizes)
            {
                _prizesList.Remove(i);
            }
            foreach (var i in persons)
            {
                _peopleList.Remove(i);
            }

            //todo：写入csv文件记录
        }
    }

    public class Prize
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Uri { get; set; }
        public bool IsUsed { get; set; }
    }

    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsUsed { get; set; }
    }
}
