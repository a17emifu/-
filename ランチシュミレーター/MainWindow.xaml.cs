using System;
using System.Collections.Generic;
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
using System.IO;
using System.Text.RegularExpressions;

namespace ランチシュミレーター
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        Lunch lunch;
        LunchList lunchList = new LunchList();
        IDKalk iDKalk = new IDKalk();

        List<string> Ingredienser = new List<string>();
        List<Lunch> slumpLunch;
        string MatNamn ="";

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void BestämNamn(string matNamn)
        {
            MatNamn = matNamn;
            textMat.Text = MatNamn;
        }
        private void BörjanIng(string ing)
        {
            if (Ingredienser.Count() == 0)
            {
                LäggIng(ing);
            }
        }
        private void LäggIng(string ingrediens)
        {
            Ingredienser.Add(ingrediens);
            VisaListBox();
        }

        private void IngDubbelChecker(string ing)
        {
            bool OkOrNot = false;
            foreach (var Ing in Ingredienser)
            {
                if (Ing == ing)
                {
                    MessageBox.Show("それはもういれたでしょ？");
                    OkOrNot = true;
                }
            }

            if (OkOrNot == false)
            {
                LäggIng(ing);
            }

        }
        private void VisaListBox()
        {
            list1.Items.Clear();
            foreach (var Ingresiens in Ingredienser)
            {
                list1.Items.Add(Ingresiens);
            }
        }

        private void RegistreraMat()
        {
            if (MatNamn == "")
            {
                MessageBox.Show("ごはんのなまえをいれよう");
            }
            else
            {
                lunch = new Lunch();
                iDKalk.RegistreraID();
                lunch.RegistreraLunch(MatNamn, Ingredienser, iDKalk.ID);
                lunchList.RegistreraList(lunch);
            }

        }

        private void VisaLunchList()
        {
            list2.Items.Clear();
            foreach(var Namn in lunchList.Lunchlist)
            {
                list2.Items.Add($"{Namn.MatID}. {Namn.MatNamn}");
            }
            { }
        }

        private void VisaMatDetaljer()
        {
            list3.Items.Clear();

            foreach (var Namn in lunchList.Lunchlist)
            {
                if (list2.SelectedItem.Equals($"{Namn.MatID}. {Namn.MatNamn}"))
                {
                    visaMatNamn.Text = Namn.MatNamn;
                    
                    foreach (var ingrediens in Namn.Ingredienser)
                    {
                        list3.Items.Add($"- {ingrediens}");
                    }
                }
            }
            
        }

        private void FörBörjaOm()
        {
            list1.Items.Clear();
            list2.SelectedIndex = -1;
            list3.Items.Clear();
            textMat.Text = "またとうろくしてね";
            visaMatNamn.Text = "";
            matNamn.Text = "";
            MatNamn= "";

            Ingredienser = new List<string>();
        }

        private void SökAvMat(string mat)
        {
            list4.Items.Clear();
            int räknare = 0;
            foreach (var Mat in lunchList.Lunchlist)
            {
                if (Mat.MatNamn == mat)
                {
                    list4.Items.Add($"{Mat.MatID}. {Mat.MatNamn}");
                    räknare++;
                }
            }
            if(räknare == 0)
            {
                MessageBox.Show("りょうりが　ないみたい・・");
            }
        }
        private void SökAvIng(string ing)
        {
            list4.Items.Clear();
            int räknare = 0;
            foreach (var Mat in lunchList.Lunchlist)
            {
                foreach (var Ing in Mat.Ingredienser)
                {
                    if (Ing == ing)
                    {
                        list4.Items.Add($"{Mat.MatID}. {Mat.MatNamn}");
                        räknare++;
                    }
                }
            }
            if (räknare == 0)
            {
                MessageBox.Show("りょうりが　ないみたい・・");
            }
        }

        private void VisaSökandeLunchList()
        {
            list5.Items.Clear();

            foreach (var Namn in lunchList.Lunchlist)
            {
                if (list4.SelectedItem.Equals($"{Namn.MatID}. {Namn.MatNamn}"))
                {
                    sökandeMatNamn.Content = Namn.MatNamn;

                    foreach (var ingrediens in Namn.Ingredienser)
                    {
                        list5.Items.Add($"- {ingrediens}");
                    }
                }
            }
        }

        private void SlumpMat()
        {
            Random rnd = new Random();
            int slumpID = rnd.Next(1,iDKalk.ID+1);
            VisaSlump(slumpID);
        }

        private void VisaSlump(int id)
        {
            list4.Items.Clear();
            list5.Items.Clear();
            string slumpMat="";

            foreach (var Mat in lunchList.Lunchlist)
            {
                if (Mat.MatID == id)
                {
                    ResultMat.Content = Mat.MatNamn;
                    list6.Items.Clear();

                    foreach (var ing in Mat.Ingredienser)
                    {
                        list6.Items.Add($"- {ing}");
                    }
                    slumpMat = Mat.MatNamn;
                }
            }

            MessageBox.Show($"きょうのごはんは・・・・ {slumpMat}だあっ！");
        }

        private void SlumpMatAvIng(string villkor)
        {
            slumpLunch = new List<Lunch>();
            
            foreach (var Mat in lunchList.Lunchlist)
            {
                foreach (var Ing in Mat.Ingredienser)
                {
                    if (Ing == villkor)
                    {
                        slumpLunch.Add(Mat);
                    }
                }
            }

            VisaSlumpAvIng();
        }

        private void VisaSlumpAvIng()
        {
            string slumpMat="";
            slumpLunch = slumpLunch.OrderBy(i => Guid.NewGuid()).ToList();
            Lunch resultLunch = new Lunch();
            resultLunch = slumpLunch[0];

            ResultMat.Content = resultLunch.MatNamn;
            list6.Items.Clear();

            foreach (var ing in resultLunch.Ingredienser)
            {
                list6.Items.Add($"- {ing}");
            }
            slumpMat = resultLunch.MatNamn;
            
            MessageBox.Show($"きょうのごはんは・・・・ {slumpMat}だあっ！");
        }

        private void SparaFil(LunchList lunchList, string filename)
        {
            FileOperations.Serialize(lunchList, filename);
        }

        private void KallaFil()
        {
            lunchList = (LunchList)FileOperations.Deserialize("filename");
            VisaLunchList();
            KallaSistaID();

        }

        private void KallaSistaID()
        {
            int sistaMatID;
            Lunch kalladLunch = new Lunch();
            kalladLunch = lunchList.Lunchlist[lunchList.Lunchlist.Count - 1];
            sistaMatID = kalladLunch.MatID;
            iDKalk.KallaID(sistaMatID);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BestämNamn(matNamn.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            IngDubbelChecker(IngNamn.Text);
            BörjanIng(IngNamn.Text);
            IngNamn.Text = "";
            IngNamn.Focus();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            RegistreraMat();
            VisaLunchList();

            FörBörjaOm();
        }

        private void list2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (list2.SelectedIndex >= 0)
            {
                VisaMatDetaljer();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            FörBörjaOm();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            list4.SelectedIndex = -1;
            if (rbtnMat.IsChecked == true)
            {
                SökAvMat(sökBox.Text);
            }
            if(rbtnIng.IsChecked == true)
            {
                SökAvIng(sökBox.Text);
            }

            list5.Items.Clear();
            sökandeMatNamn.Content = "";
        }

        private void list4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (list4.SelectedIndex >= 0)
            {
                VisaSökandeLunchList();
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            sökandeMatNamn.Content = "";
            if ((villHaIng.Text== "（オプション）") || (villHaIng.Text == ""))
            {
                SlumpMat();
            }
            else
            {
                SlumpMatAvIng(villHaIng.Text);
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            SparaFil(lunchList, "filename");
            MessageBox.Show("セーブかんりょう！");
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            KallaFil();
        }
    }
}
