using System.Runtime.InteropServices;
using System.Threading.Tasks.Dataflow;
using System.Web;

namespace gui2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtCount.Text = Properties.Settings.Default.txtCount.ToString();
            txtGrades.Text = Properties.Settings.Default.txtGrades;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n=int.Parse(txtCount.Text);
            string grades = txtGrades.Text;
            string result = Logic.Grade(n, grades);
          
            MessageBox.Show(result);
            Properties.Settings.Default.txtCount = n;
            Properties.Settings.Default.txtGrades = grades;
            Properties.Settings.Default.Save();
        }

        public class Logic
        {
            public static string Grade(int count, string grades)
            {
                string[] gradeInput=grades.Split(" ");
      
                if (count <= 0)
                {
                    return "Количетсво учеников должно быть больше 0";
                }
                if (gradeInput.Length != count)
                {
                    return "количество учеников не совпадает с количеством оценок";
                }
                int[] gradeArray = new int[count];


                for (int i = 0; i < count; i++)
                {
                    gradeArray[i] = int.Parse(gradeInput[i]);
                    if (gradeArray[i] < 2 || gradeArray[i] > 5)
                    {
                        return "Оценка должна быть от 2 до 5";
                        
                    }
                }

                Statistic stats = new Statistic();

                foreach (int grade in gradeArray)
                {
                    switch (grade)
                    {
                        case 5: stats.Count5++; break;
                        case 4: stats.Count4++; break;
                        case 3: stats.Count3++; break;
                        case 2: stats.Count2++; break;
                    }
                }
              
                return ("Пятерок: " + stats.Count5 + "\n") +
                            ("Четверок: " + stats.Count4 + "\n") +
                            ("Троек: " + stats.Count3 + "\n") +
                            ("Двоек: " + stats.Count2 + "\n"); ;

            }
        }
        public struct Statistic
        {
            public int Count2;
            public int Count3;
            public int Count4;
            public int Count5;
        }
    }
}
