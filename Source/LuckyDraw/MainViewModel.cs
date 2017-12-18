using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
                return _prizesList.Where(x => x.IsUsed == false).Count();
            }
        }

        /// <summary>
        /// 剩余待抽奖人数
        /// </summary>
        public int PeopleCount
        {
            get
            {
                return _peopleList.Where(x => x.IsUsed == false).Count();
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

        /// <summary>
        /// 随机取得奖品
        /// </summary>
        public Prize RandomPrize
        {
            get
            {
                var list = _prizesList.Where(x => x.IsUsed == false).ToList();
                if (list.Count > 0)
                {
                    Random random = new Random();
                    var prize = list[random.Next(list.Count)];
                    prize.IsUsed = true;
                    return prize;
                }

                return null;
            }
        }

        /// <summary>
        /// 随机取得得奖者
        /// </summary>
        public Person RandomPerson
        {
            get
            {
                var list = _peopleList.Where(x => x.IsUsed == false).ToList();
                if (list.Count > 0)
                {
                    Random random = new Random();
                    var person = list[random.Next(list.Count)];
                    person.IsUsed = true;
                    return person;
                }

                return null;
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
            for (int i = 1; i <= 12; i++)
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
                    ID = i,
                    Name = "Persion_" + i.ToString(),
                    IsUsed = false
                });
            }
        }

        public bool WriteData(Dictionary<Prize, Person> list)
        {
            try
            {
                //写入csv文件记录
                StreamWriter writer = new StreamWriter("result.csv", true, Encoding.UTF8);
                foreach (var i in list)
                {
                    string temp = i.Key.ID + "," + i.Value.ID + ","
                        + i.Key.Name + "," + i.Value.Name;
                    writer.WriteLine(temp);
                }

                writer.Flush();
                writer.Close();

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("存储信息失败，请检查得奖结果文件是否被占用!" +
                    "\n\r"+"本次抽奖不生效！");
                return false;
            }
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
