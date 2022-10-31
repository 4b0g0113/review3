using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace review3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string,int> foods=new Dictionary<string,int>();
        Dictionary<string,int> order=new Dictionary<string,int>();
        public MainWindow()
        {
            InitializeComponent();
            AddNewFood(foods);
        }

        private void AddNewFood(Dictionary<string, int> myfood)
        {
            myfood.Add("大麥克漢堡(大)", 150);
            myfood.Add("大麥克漢堡(小)",  90);
            myfood.Add("麥香雞漢堡(大)", 140);
            myfood.Add("麥香雞漢堡(小)",  80);
            myfood.Add("雙層牛肉堡(大)", 160);
            myfood.Add("雙層牛肉堡(小)", 100);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var targetTextBox = sender as TextBox;
            bool success = int.TryParse(targetTextBox.Text, out int count);
            if (!success)
                MessageBox.Show("輸入非整數", "輸入錯誤");
            else if (count <= 0)
                MessageBox.Show("輸入非正整數", "輸入錯誤");
            else
            {
                StackPanel targetStackPanel = targetTextBox.Parent as StackPanel;
                Label targetLabel = targetStackPanel.Children[0] as Label;
                string foodItem =targetLabel.Content.ToString();
                int ItemPrice = foods[foodItem];
                //MessageBox.Show($"{foodItem}x{count},每份{ItemPrice}元,共{ItemPrice * count}元");
                if(order.ContainsKey(foodItem))
                    order.Remove(foodItem);
                order.Add(foodItem,count);  
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int totalAmount = 0;
            int sellAmount = 0;
            int feedbackPoint = 0;
            string displayScreen="訂購清單如下:\n";
            foreach(KeyValuePair<string,int> item in order)
            {
                string foodItem=item.Key;
                int itemCount = item.Value;
                int itemPrice = foods[foodItem];
                displayScreen += $"{foodItem}x{itemCount},每份{itemPrice}元,共{itemCount * itemPrice}元。\n";
                totalAmount += itemCount * itemPrice;
            }
            feedbackPoint = Convert.ToInt32(Math.Round(Convert.ToDouble(totalAmount) * 0.1));
            string discountMessenge = "";
            if(totalAmount>=1000)
            {
                sellAmount = Convert.ToInt32(Math.Round(Convert.ToDouble(totalAmount) * 0.85));
                discountMessenge = "總金額滿1000可打85折";
            }else if(totalAmount>=500)
            {
                sellAmount = Convert.ToInt32(Math.Round(Convert.ToDouble(totalAmount) * 0.9));
                discountMessenge = "總金額滿500可打9折";
            }else
            {
                sellAmount = totalAmount;
                discountMessenge = "未滿500元,不打折";
            }
            displayScreen +="\n本次訂單如下\n";
            displayScreen += $"原價{totalAmount}元 {discountMessenge} \n售價{sellAmount}元。\n";
            displayScreen += $"回饋點數{feedbackPoint}點。\n";
            display.Content = displayScreen;
        }
        

    }
}
