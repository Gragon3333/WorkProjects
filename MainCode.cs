using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using GoogleApi;
using System.Windows.Forms.DataVisualization.Charting;

namespace Kursach
{
    public partial class Form1 : Form
    {
        public class AirPollution
        {
            public AirPollution(int aqi, string co, string pm2, string pm10, DateTime tm)
                => (this.aqi, CO, PM2and5, PM10, date) = (aqi, co, pm2, pm10, tm);

            public int aqi;
            public string CO;
            public string PM2and5;
            public string PM10;
            public DateTime date;

        }

        public class Root
        {
            public string Post { get; set; }
            public string PostLat { get; set; }
            public string PostLon { get; set; }
            public List<AirPollution> Indication { get; set; }
        }

        string ConnStr = @"Data Source=DESKTOP-U184JJA\SQLEXPRESS;Initial Catalog=Pollution;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 500000000;
            timer1.Enabled = true;
            timer1.Tick += new EventHandler(timer1_Tick);
            
            
        }

        public void MyExecuteNonQuery(string sqltext)
        {
            SqlConnection cn;
            SqlCommand cmd;

            cn = new SqlConnection(ConnStr);
            cn.Open();
            cmd = cn.CreateCommand();
            cmd.CommandText = sqltext;
            cmd.ExecuteNonQuery();
            cn.Close();
        }
     

 
        private void timer1_Tick(object sender, EventArgs e)
        {
            string path = File.ReadAllText("FilePath.txt");
            StreamReader read = new StreamReader(path);
            string dd = read.ReadLine();
            read.Close();
            List<Root> newRoots = JsonConvert.DeserializeObject<List<Root>>(dd);
            int last = newRoots[0].Indication.Count;
            string[] pos = new string[] { "Пост Кирова", "Пост Маршала Жукова", "Пост Ленина", "Пост Первомайская", "Пост Крылова" };
            var indication = new AirPollution[1];
            DateTime time = DateTime.Now;
            Random rand = new Random();
            Random randAQI = new Random();
            for (int i = 0; i < pos.Length; i++)
            {
                int ct = i + 1;
                int counter = dataGridView1.Rows.Count;
                string sqltext = "INSERT INTO [AirPollutions] ([ObservationPost], [AQI], [CO], [PM2.5], [PM10], [date]) VALUES(";
                sqltext += "\'" + ct + "\',";
                sqltext += "\'" + newRoots[i].Indication[last-1].aqi + "\',";
                sqltext += "\'" + newRoots[i].Indication[last-1].CO + "\',";
                sqltext += "\'" + newRoots[i].Indication[last-1].PM2and5 + "\',";
                sqltext += "\'" + newRoots[i].Indication[last-1].PM10 + "\',";
                sqltext += "\'" + newRoots[i].Indication[last-1].date + "\')";
                MyExecuteNonQuery(sqltext);
                if (counter < 7 )
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = i + 1.ToString(); ;
                    dataGridView1.Rows[i].Cells[1].Value = pos[i];
                    dataGridView1.Rows[i].Cells[2].Value = newRoots[i].Indication[last - 1].aqi.ToString();
                    dataGridView1.Rows[i].Cells[3].Value = newRoots[i].Indication[last - 1].CO.ToString();
                    dataGridView1.Rows[i].Cells[4].Value = newRoots[i].Indication[last - 1].PM2and5.ToString();
                    dataGridView1.Rows[i].Cells[5].Value = newRoots[i].Indication[last - 1].PM10.ToString();
                    dataGridView1.Rows[i].Cells[6].Value = newRoots[i].Indication[last - 1].date.ToString();
                }
                else
                {
                    dataGridView1.Rows[i].Cells[0].Value = i + 1.ToString(); ;
                    dataGridView1.Rows[i].Cells[1].Value = pos[i];
                    dataGridView1.Rows[i].Cells[2].Value = newRoots[i].Indication[last - 1].aqi.ToString();
                    dataGridView1.Rows[i].Cells[3].Value = newRoots[i].Indication[last - 1].CO.ToString();
                    dataGridView1.Rows[i].Cells[4].Value = newRoots[i].Indication[last - 1].PM2and5.ToString();
                    dataGridView1.Rows[i].Cells[5].Value = newRoots[i].Indication[last - 1].PM10.ToString();
                    dataGridView1.Rows[i].Cells[6].Value = newRoots[i].Indication[last - 1].date.ToString();

                }
               


            }
            int fromVeryLowToVeryHighAQI = randAQI.Next(1, 5);
            for (int i = 0; i < pos.Length; i++)
            {
                for (int j = 0; j < indication.Length; j++)
                {
                    if (fromVeryLowToVeryHighAQI == 1)
                    {
                        indication[j] = new AirPollution(rand.Next(0, 100), Convert.ToString(rand.Next(0, 20) + "%"), Convert.ToString(rand.Next(0, 20) + "%"), Convert.ToString(rand.Next(0, 20) + "%"), time);
                        newRoots[i].Indication.Add(indication[j]);

                    }
                    if (fromVeryLowToVeryHighAQI == 2)
                    {
                        indication[j] = new AirPollution(rand.Next(101, 200), Convert.ToString(rand.Next(21, 40) + "%"), Convert.ToString(rand.Next(21, 40) + "%"), Convert.ToString(rand.Next(21, 40) + "%"), time);
                        newRoots[i].Indication.Add(indication[j]);
                    }
                    if (fromVeryLowToVeryHighAQI == 3)
                    {
                        indication[j] = new AirPollution(rand.Next(201, 300), Convert.ToString(rand.Next(41, 60) + "%"), Convert.ToString(rand.Next(41, 60) + "%"), Convert.ToString(rand.Next(41, 60) + "%"), time);
                        newRoots[i].Indication.Add(indication[j]);
                    }
                    if (fromVeryLowToVeryHighAQI == 4)
                    {
                        indication[j] = new AirPollution(rand.Next(301, 400), Convert.ToString(rand.Next(61, 80) + "%"), Convert.ToString(rand.Next(61, 80) + "%"), Convert.ToString(rand.Next(61, 80) + "%"), time);
                        newRoots[i].Indication.Add(indication[j]);

                    }
                    if (fromVeryLowToVeryHighAQI == 5)
                    {
                        indication[j] = new AirPollution(rand.Next(401, 500), Convert.ToString(rand.Next(81, 100) + "%"), Convert.ToString(rand.Next(81, 100) + "%"), Convert.ToString(rand.Next(81, 100) + "%"), time);
                        newRoots[i].Indication.Add(indication[j]);
                    }
                }
            }
            string js = JsonConvert.SerializeObject(newRoots);
            StreamWriter writer = new StreamWriter(path);
            writer.WriteLine(js);
            writer.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            string path = File.ReadAllText("FilePath.txt");
            StreamReader read = new StreamReader(path);
            string dd = read.ReadLine();
            read.Close();
            List<Root> newRoots = JsonConvert.DeserializeObject<List<Root>>(dd);
            int last = newRoots[0].Indication.Count;
            string[] pos = new string[] { "Пост Кирова", "Пост Маршала Жукова", "Пост Ленина", "Пост Первомайская", "Пост Крылова" };
            DataGridViewTextBoxColumn id = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn post = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn aqi = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn CO = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn pm2and5 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn pm10 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn date = new DataGridViewTextBoxColumn();
            id.HeaderText = "ID";
            post.HeaderText = "Пост";
            post.Width = 200;
            aqi.HeaderText = "AQI";
            CO.HeaderText = "CO\n(норма 3 мг/м^3)";
            CO.Width = 150;
            pm2and5.HeaderText = "PM2.5\n(норма 0.035 мг/м^3)";
            pm2and5.Width = 150;
            pm10.HeaderText = "PM10\n(норма 0.035 мг/м^3)";
            pm10.Width = 150;
            date.HeaderText = "Дата";
            date.Width = 200;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { id, post, aqi, CO, pm2and5, pm10, date });
            for (int i = 0; i < pos.Length; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = i + 1.ToString(); ;
                dataGridView1.Rows[i].Cells[1].Value = pos[i];
                dataGridView1.Rows[i].Cells[2].Value = newRoots[i].Indication[last - 1].aqi.ToString();
                dataGridView1.Rows[i].Cells[3].Value = newRoots[i].Indication[last - 1].CO.ToString();
                dataGridView1.Rows[i].Cells[4].Value = newRoots[i].Indication[last - 1].PM2and5.ToString();
                dataGridView1.Rows[i].Cells[5].Value = newRoots[i].Indication[last - 1].PM10.ToString();
                dataGridView1.Rows[i].Cells[6].Value = newRoots[i].Indication[last - 1].date.ToString();
            }
        }

        private void просмотретьВсеЗаписиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 frm5 = new Form5();
            frm5.Show();
        }
        
        private void гистограммаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = File.ReadAllText("FilePath.txt");
            StreamReader read = new StreamReader(path);
            string dd = read.ReadLine();
            read.Close();
            List<Root> newRoots = JsonConvert.DeserializeObject<List<Root>>(dd);
            int last = newRoots[0].Indication.Count;
            string[] pos = new string[] { "Пост Кирова", "Пост Маршала Жукова", "Пост Ленина", "Пост Первомайская", "Пост Крылова" };
            Form4 frm4 = new Form4();
            frm4.chart1.Series.Clear();
            frm4.chart1.Series.Add("Индекс качества воздуха(AQI)");
            for (int i = 0; i < pos.Length; i++)
            {
                frm4.chart1.Series["Индекс качества воздуха(AQI)"].Points.AddXY(pos[i], newRoots[i].Indication[last - 1].aqi);
            }
            frm4.label2.Text = "Текущий индекс качества воздуха";
            frm4.label1.Text = "Значение индекса качества воздуха";
            frm4.label1.Location = new Point(25,150);
            frm4.Show();
            

        }

        private void графикПоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = File.ReadAllText("FilePath.txt");
            StreamReader read = new StreamReader(path);
            string dd = read.ReadLine();
            read.Close();
            List<Root> newRoots = JsonConvert.DeserializeObject<List<Root>>(dd);
            int last = newRoots[0].Indication.Count;
            string[] pos = new string[] { "Пост Кирова", "Пост Маршала Жукова", "Пост Ленина", "Пост Первомайская", "Пост Крылова" };
            Form4 frm4 = new Form4();
            frm4.chart1.Series.Clear();
            frm4.chart1.Series.Add("Монооксид углерода(CO), %");
            for (int i = 0; i < pos.Length; i++)
            {
                int CO = Convert.ToInt32(newRoots[i].Indication[last - 1].CO.Replace("%", ""));
                frm4.chart1.Series["Монооксид углерода(CO), %"].Points.AddXY(pos[i], CO);
            }
            frm4.label1.Text = "% от ПДК";
            frm4.label2.Text = "Текущее содержание монооксида углерода";
            frm4.label2.Location = new Point(250, 25);
            frm4.label1.Location = new Point(25, 250);
            frm4.Show();
        }

        private void графикПоВзвешеннымЧастицамДо25МкмPM25ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = File.ReadAllText("FilePath.txt");
            StreamReader read = new StreamReader(path);
            string dd = read.ReadLine();
            read.Close();
            List<Root> newRoots = JsonConvert.DeserializeObject<List<Root>>(dd);
            int last = newRoots[0].Indication.Count;
            string[] pos = new string[] { "Пост Кирова", "Пост Маршала Жукова", "Пост Ленина", "Пост Первомайская", "Пост Крылова" };
            Form4 frm4 = new Form4();
            frm4.chart1.Series.Clear();
            frm4.chart1.Series.Add("Взвешенные частицы до 2.5 мкм(PM2.5), %");
            for (int i = 0; i < pos.Length; i++)
            {
                int PM2and5 = Convert.ToInt32(newRoots[i].Indication[last - 1].PM2and5.Replace("%", ""));
                frm4.chart1.Series["Взвешенные частицы до 2.5 мкм(PM2.5), %"].Points.AddXY(pos[i], PM2and5);
            }
            frm4.label2.Text = "Текущее содержание взвешенных частиц до 2.5 мкм";
            frm4.label1.Text = "% от ПДК";
            frm4.label2.Location = new Point(230, 25);
            frm4.label1.Location = new Point(25, 250);
            frm4.Show();
        }

        private void графикПоВзвешеннымЧастицамДо10МкмPM10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = File.ReadAllText("FilePath.txt");
            StreamReader read = new StreamReader(path);
            string dd = read.ReadLine();
            read.Close();
            List<Root> newRoots = JsonConvert.DeserializeObject<List<Root>>(dd);
            int last = newRoots[0].Indication.Count;
            string[] pos = new string[] { "Пост Кирова", "Пост Маршала Жукова", "Пост Ленина", "Пост Первомайская", "Пост Крылова" };
            Form4 frm4 = new Form4();
            frm4.chart1.Series.Clear();
            frm4.chart1.Series.Add("Взвешенные частицы до 10 мкм(PM10), %");
            for (int i = 0; i < pos.Length; i++)
            {
                int PM10 = Convert.ToInt32(newRoots[i].Indication[last - 1].PM10.Replace("%", ""));
                frm4.chart1.Series["Взвешенные частицы до 10 мкм(PM10), %"].Points.AddXY(pos[i], PM10);
            }
            frm4.label2.Text = "Текущее содержание взвешенных частиц до 10 мкм";
            frm4.label1.Text = "% от ПДК";
            frm4.label2.Location = new Point(230, 25);
            frm4.label1.Location = new Point(25, 250);
            frm4.Show();
        }

        private void графикСреднихЗначенийИндексаКачесвтаВоздухаAQIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = File.ReadAllText("FilePath.txt");
            StreamReader read = new StreamReader(path);
            string dd = read.ReadLine();
            read.Close();
            int sum = 0;
            int aver = 0;
            List<Root> newRoots = JsonConvert.DeserializeObject<List<Root>>(dd);
            int last = newRoots[0].Indication.Count;
            string[] pos = new string[] { "Пост Кирова", "Пост Маршала Жукова", "Пост Ленина", "Пост Первомайская", "Пост Крылова" };
            Form4 frm4 = new Form4();
            frm4.chart1.Series.Clear();
            frm4.chart1.Series.Add("Индекс качества воздуха(AQI)");
            //frm4.chart1.Series["Индекс качества воздуха(AQI)"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            
            for (int i = 0; i < pos.Length; i++)
            {
                for (int j = 0; j < last; j++)
                {
                    sum += newRoots[i].Indication[j].aqi;
                }
                aver = sum / last;
                frm4.chart1.Series["Индекс качества воздуха(AQI)"].Points.AddXY(pos[i], aver);
                sum = 0;
                
            }
            frm4.label2.Text = "Средние значения индекса качества воздуха";
            frm4.label1.Text = "Значение индекса качества воздуха";
            frm4.label1.Location = new Point(25, 150);
            frm4.label2.Location = new Point(230, 25);
            frm4.Show();
        }

        

        private void графикСреднихЗначенийМонооксидаУглеродаCOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = File.ReadAllText("FilePath.txt");
            StreamReader read = new StreamReader(path);
            string dd = read.ReadLine();
            read.Close();
            int sum = 0;
            int aver = 0;
            List<Root> newRoots = JsonConvert.DeserializeObject<List<Root>>(dd);
            int last = newRoots[0].Indication.Count;
            string[] pos = new string[] { "Пост Кирова", "Пост Маршала Жукова", "Пост Ленина", "Пост Первомайская", "Пост Крылова" };
            Form4 frm4 = new Form4();
            frm4.chart1.Series.Clear();
            frm4.chart1.Series.Add("Монооксид углерода(CO), %");
            //frm4.chart1.Series["Монооксид углерода(CO), %"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            for (int i = 0; i < pos.Length; i++)
            {
                for (int j = 0; j < last; j++)
                {
                    int CO = Convert.ToInt32(newRoots[i].Indication[j].CO.Replace("%", ""));
                    sum += CO;
                }
                aver = sum / last;
                frm4.chart1.Series["Монооксид углерода(CO), %"].Points.AddXY(pos[i], aver);
                sum = 0;

            }
            frm4.label1.Text = "% от ПДК";
            frm4.label2.Text = "Средний процент монооксида углерода в воздухе";
            frm4.label2.Location = new Point(230, 25);
            frm4.label1.Location = new Point(25, 250);
            frm4.Show();
        }

        private void графикСреднихЗначенToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = File.ReadAllText("FilePath.txt");
            StreamReader read = new StreamReader(path);
            string dd = read.ReadLine();
            read.Close();
            int sum = 0;
            int aver = 0;
            List<Root> newRoots = JsonConvert.DeserializeObject<List<Root>>(dd);
            int last = newRoots[0].Indication.Count;
            string[] pos = new string[] { "Пост Кирова", "Пост Маршала Жукова", "Пост Ленина", "Пост Первомайская", "Пост Крылова" };
            Form4 frm4 = new Form4();
            frm4.chart1.Series.Clear();
            frm4.chart1.Series.Add("Взвешенные частицы до 2.5 мкм(PM2.5), %");
            //frm4.chart1.Series["Взвешенные частицы до 2.5 мкм(PM2.5), %"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            for (int i = 0; i < pos.Length; i++)
            {
                for (int j = 0; j < last; j++)
                {
                    int PM2and5 = Convert.ToInt32(newRoots[i].Indication[j].PM2and5.Replace("%", ""));
                    sum += PM2and5;
                }
                aver = sum / last;
                frm4.chart1.Series["Взвешенные частицы до 2.5 мкм(PM2.5), %"].Points.AddXY(pos[i], aver);
                sum = 0;

            }
            frm4.label2.Text = "Средний процент взвешенных частиц до 2.5 мкм в воздухе";
            frm4.label1.Text = "% от ПДК";
            frm4.label2.Location = new Point(200, 25);
            frm4.label1.Location = new Point(25, 250);
            frm4.Show();
        }

        private void графикСреднихЗначенийPM10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = File.ReadAllText("FilePath.txt");
            StreamReader read = new StreamReader(path);
            string dd = read.ReadLine();
            read.Close();
            int sum = 0;
            int aver = 0;
            List<Root> newRoots = JsonConvert.DeserializeObject<List<Root>>(dd);
            int last = newRoots[0].Indication.Count;
            string[] pos = new string[] { "Пост Кирова", "Пост Маршала Жукова", "Пост Ленина", "Пост Первомайская", "Пост Крылова" };
            Form4 frm4 = new Form4();
            frm4.chart1.Series.Clear();
            frm4.chart1.Series.Add("Взвешенные частицы до 10 мкм(PM10), %");
            //frm4.chart1.Series["Взвешенные частицы до 10 мкм(PM10), %"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            for (int i = 0; i < pos.Length; i++)
            {
                for (int j = 0; j < last; j++)
                {
                    int PM10 = Convert.ToInt32(newRoots[i].Indication[j].PM10.Replace("%", ""));
                    sum += PM10;
                }
                aver = sum / last;
                frm4.chart1.Series["Взвешенные частицы до 10 мкм(PM10), %"].Points.AddXY(pos[i], aver);
                sum = 0;
                frm4.label2.Text = "Средний процент взвешенных частиц до 10 мкм в воздухе";
                frm4.label1.Text = "% от ПДК";
                frm4.label2.Location = new Point(200, 25);
                frm4.label1.Location = new Point(25, 250);
            }
            frm4.Show();
        }

        private void техническоеОбслуживаниеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ConnStr = @"Data Source=DESKTOP-U184JJA\SQLEXPRESS;Initial Catalog=Pollution;Integrated Security=True";
            string sqltext;
            sqltext = "Select [TechnicalService].[ID], [TechnicalService].[NumberOfTechnician], [Technicians].[Surname], [Technicians].[Name]";
            sqltext += ",[Technicians].[Patronymic], [ModelsOfSensors].[NameOfSensor]";
            sqltext += ",[KindsOfTechnicalService].[Kind], [TechnicalService].[DateOfTechnicalService]";
            sqltext += "From [TechnicalService] Inner Join [Technicians]";
            sqltext += "on [TechnicalService].[NumberOfTechnician] = [Technicians].[ID]";
            sqltext += "Inner Join [ModelsOfSensors]";
            sqltext += "on TechnicalService.Sensor = [ModelsOfSensors].[ID]";
            sqltext += "Inner Join [KindsOfTechnicalService]";
            sqltext += "on [TechnicalService].[KindOfTechnicalService] = [KindsOfTechnicalService].[ID]";
            Form2 f = new Form2();
            SqlDataAdapter da = new SqlDataAdapter(sqltext, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "[TechnicalService]");
            f.dataGridView1.DataSource = ds.Tables["[TechnicalService]"].DefaultView;
            f.dataGridView1.Columns[1].Width = 150;
            f.dataGridView1.Columns[6].Width = 250;
            f.dataGridView1.Columns[7].Width = 200;
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        

        private void техническоеОбслуживаниеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string ConnStr = @"Data Source=DESKTOP-U184JJA\SQLEXPRESS;Initial Catalog=Pollution;Integrated Security=True";
            string sqltext;
            sqltext = "Select [TechnicalService].[ID], [TechnicalService].[NumberOfTechnician], [Technicians].[Surname], [Technicians].[Name]";
            sqltext += ",[Technicians].[Patronymic], [ModelsOfSensors].[NameOfSensor]";
            sqltext += ",[KindsOfTechnicalService].[Kind], [TechnicalService].[DateOfTechnicalService]";
            sqltext += "From [TechnicalService] Inner Join [Technicians]";
            sqltext += "on [TechnicalService].[NumberOfTechnician] = [Technicians].[ID]";
            sqltext += "Inner Join [ModelsOfSensors]";
            sqltext += "on TechnicalService.Sensor = [ModelsOfSensors].[ID]";
            sqltext += "Inner Join [KindsOfTechnicalService]";
            sqltext += "on [TechnicalService].[KindOfTechnicalService] = [KindsOfTechnicalService].[ID]";
            Form2 f = new Form2();
            SqlDataAdapter da = new SqlDataAdapter(sqltext, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "[TechnicalService]");
            f.dataGridView1.DataSource = ds.Tables["[TechnicalService]"].DefaultView;
            f.dataGridView1.Columns[1].Width = 150;
            f.dataGridView1.Columns[6].Width = 250;
            f.dataGridView1.Columns[7].Width = 200;
            f.Show();
        }

        private void загрязнениВоздухаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ConnStr = @"Data Source=DESKTOP-U184JJA\SQLEXPRESS;Initial Catalog=Pollution;Integrated Security=True";
            string sqltext;
            sqltext = "Select [AirPollutions].[ID], [AirPollutions].[AQI], [AirPollutions].[CO], [AirPollutions].[PM2.5], [AirPollutions].[PM10], [ObservationPosts].[NameOfPost], [AirPollutions].[Date]";
            sqltext += "From [AirPollutions]";
            sqltext += "inner join [ObservationPosts] on [AirPollutions].[ObservationPost] = [ObservationPosts].[ID]";
            Form5 f = new Form5();
            SqlDataAdapter da = new SqlDataAdapter(sqltext, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "[AirPollutions]");
            f.dataGridView1.DataSource = ds.Tables["[AirPollutions]"].DefaultView;
            f.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void техникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqltext;
            string ConnStr = @"Data Source=DESKTOP-U184JJA\SQLEXPRESS;Initial Catalog=Pollution;Integrated Security=True";
            sqltext = "Select [Technicians].[ID], [Technicians].[Surname], [Technicians].[Name], [Technicians].[Surname], [Technicians].[DateOfBirth], [Ranks].[Rank]";
            sqltext += "From [Technicians]";
            sqltext += "Inner Join [Ranks]";
            sqltext += "on [Technicians].[Rank] = [Ranks].[ID]";
            Form3 f = new Form3();
            SqlDataAdapter da = new SqlDataAdapter(sqltext, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "[Technicians]");
            f.dataGridView1.DataSource = ds.Tables["[Technicians]"].DefaultView;
            f.Show();
        }

        private void видыТезническогоОбслуживанияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqltext;
            string ConnStr = @"Data Source=DESKTOP-U184JJA\SQLEXPRESS;Initial Catalog=Pollution;Integrated Security=True";
            sqltext = "Select * From [KindsOfTechnicalService]";
            Form8 f = new Form8();
            SqlDataAdapter da = new SqlDataAdapter(sqltext, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "[KindsOfTechnicalService]");
            f.dataGridView1.DataSource = ds.Tables["[KindsOfTechnicalService]"].DefaultView;
            f.dataGridView1.Columns[1].Width = 250;
            f.Show();
        }

        private void разрядыТехниковToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqltext;
            string ConnStr = @"Data Source=DESKTOP-U184JJA\SQLEXPRESS;Initial Catalog=Pollution;Integrated Security=True";
            sqltext = "Select * From [Ranks]";
            Form10 f = new Form10();
            SqlDataAdapter da = new SqlDataAdapter(sqltext, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "[Ranks]");
            f.dataGridView1.DataSource = ds.Tables["[Ranks]"].DefaultView;
            f.dataGridView1.Columns[1].Width = 120;
            f.Show();
        }

        private void датчикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqltext;
            string ConnStr = @"Data Source=DESKTOP-U184JJA\SQLEXPRESS;Initial Catalog=Pollution;Integrated Security=True";
            sqltext = "Select [Sensors].[ID], [ModelsOfSensors].[NameOfSensor], [Sensors].[InventoryNumber], [Sensors].[DateOfPurchase]";
            sqltext += "from [Sensors ]";
            sqltext += "Inner Join [ModelsOfSensors]";
            sqltext += "on [Sensors].[Sensor] = [ModelsOfSensors].[ID]";
            Form9 f = new Form9();
            SqlDataAdapter da = new SqlDataAdapter(sqltext, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "[Sensors]");
            f.dataGridView1.DataSource = ds.Tables["[Sensors]"].DefaultView;
            f.dataGridView1.Columns[1].Width = 120;
            f.dataGridView1.Columns[2].Width = 120;
            f.dataGridView1.Columns[3].Width = 120;
            f.Show();
        }

        private void моделиДатчиковToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqltext;
            string ConnStr = @"Data Source=DESKTOP-U184JJA\SQLEXPRESS;Initial Catalog=Pollution;Integrated Security=True";
            sqltext = "Select [ModelsOfSensors].[Id], [ModelsOfSensors].[NameOfSensor], [ModelsOfSensors].[SerialNumber]";
            sqltext += ",[KindsOfSensor].[Kind], [Manufacturers].[Manufacturer], [Appointments].[Appointment], [DegreesOfProtection].[DegreeOfProtection]";
            sqltext += ",[ModelsOfSensors].[EnergyConsumption], [ModelsOfSensors].[GuaranteePeriod], [ModelsOfSensors].[DateOfRelease]";
            sqltext += ",[ModelsOfSensors].[Price]";
            sqltext += "From [ModelsOfSensors]";
            sqltext += "inner join [KindsOfSensor] on [ModelsOfSensors].[KindOfSensor] = [KindsOfSensor].[ID]";
            sqltext += "inner join [Manufacturers] on [ModelsOfSensors].[Manufacturer] = [Manufacturers].[ID]";
            sqltext += "inner join [Appointments] on [ModelsOfSensors].[Appointment] = [Appointments].[ID]";
            sqltext += "inner join [DegreesOfProtection] on [ModelsOfSensors].[DegreeOfProtection] = [DegreesOfProtection].[ID]";
            Form11 f = new Form11();
            SqlDataAdapter da = new SqlDataAdapter(sqltext, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "[ModelsOfSensors]");
            f.dataGridView1.DataSource = ds.Tables["[ModelsOfSensors]"].DefaultView;
            f.dataGridView1.Columns[3].Width = 150;
            f.dataGridView1.Columns[5].Width = 250;
            f.dataGridView1.Columns[6].Width = 150;
            f.dataGridView1.Columns[7].Width = 150;
            f.Show();
        }

        private void видДатчикаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqltext;
            string ConnStr = @"Data Source=DESKTOP-U184JJA\SQLEXPRESS;Initial Catalog=Pollution;Integrated Security=True";
            sqltext = "Select * From [KindsOfSensor]";
            Form12 f = new Form12();
            SqlDataAdapter da = new SqlDataAdapter(sqltext, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "[KindsOfSensor]");
            f.dataGridView1.DataSource = ds.Tables["[KindsOfSensor]"].DefaultView;
            f.dataGridView1.Columns[1].Width = 200;
            f.Show();
        }

        private void степеньЗащитыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqltext;
            string ConnStr = @"Data Source=DESKTOP-U184JJA\SQLEXPRESS;Initial Catalog=Pollution;Integrated Security=True";
            sqltext = "Select * From [DegreesOfProtection]";
            Form13 f = new Form13();
            SqlDataAdapter da = new SqlDataAdapter(sqltext, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "[DegreesOfProtection]");
            f.dataGridView1.DataSource = ds.Tables["[DegreesOfProtection]"].DefaultView;
            f.dataGridView1.Columns[1].Width = 150;
            f.Show();
        }

        private void назначениеДатчикаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqltext;
            string ConnStr = @"Data Source=DESKTOP-U184JJA\SQLEXPRESS;Initial Catalog=Pollution;Integrated Security=True";
            sqltext = "Select * From [Appointments]";
            Form14 f = new Form14();
            SqlDataAdapter da = new SqlDataAdapter(sqltext, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "[Appointments]");
            f.dataGridView1.DataSource = ds.Tables["[Appointments]"].DefaultView;
            f.dataGridView1.Columns[1].Width = 250;
            f.Show();
        }

        private void производительToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqltext;
            string ConnStr = @"Data Source=DESKTOP-U184JJA\SQLEXPRESS;Initial Catalog=Pollution;Integrated Security=True";
            sqltext = "Select [Manufacturers].[ID], [Manufacturers].[Manufacturer], [Countries].[Country]";
            sqltext += "from [Manufacturers]";
            sqltext += "inner join [Countries] on [Manufacturers].[Country] = [Countries].[ID]";
            Form15 f = new Form15();
            SqlDataAdapter da = new SqlDataAdapter(sqltext, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "[Manufacturers]");
            f.dataGridView1.DataSource = ds.Tables["[Manufacturers]"].DefaultView;
            f.dataGridView1.Columns[1].Width = 150;
            f.Show();
        }

        private void странаПроизводителяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqltext;
            string ConnStr = @"Data Source=DESKTOP-U184JJA\SQLEXPRESS;Initial Catalog=Pollution;Integrated Security=True";
            sqltext = "Select * From [Countries]";
            Form16 f = new Form16();
            SqlDataAdapter da = new SqlDataAdapter(sqltext, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "[Countries]");
            f.dataGridView1.DataSource = ds.Tables["[Countries]"].DefaultView;
            f.dataGridView1.Columns[1].Width = 150;
            f.Show();
        }

        private void датчикиНаПостуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqltext;
            string ConnStr = @"Data Source=DESKTOP-U184JJA\SQLEXPRESS;Initial Catalog=Pollution;Integrated Security=True";
            sqltext = "Select [SensorsOnPost].[ID], [ObservationPosts].[NameOfPost], [ModelsOfSensors].[NameOfSensor]";
            sqltext += "From [SensorsOnPost]";
            sqltext += "inner join [ObservationPosts] on [SensorsOnPost].[Post] = [ObservationPosts].[ID]";
            sqltext += "inner join [ModelsOfSensors] on [SensorsOnPost].[Sensor] = [ModelsOfSensors].[ID]";
            Form17 f = new Form17();
            SqlDataAdapter da = new SqlDataAdapter(sqltext, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "[SensorsOnPost]");
            f.dataGridView1.DataSource = ds.Tables["[SensorsOnPost]"].DefaultView;
            f.dataGridView1.Columns[1].Width = 150;
            f.Show();
        }

        private void постыНаблюденияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqltext;
            string ConnStr = @"Data Source=DESKTOP-U184JJA\SQLEXPRESS;Initial Catalog=Pollution;Integrated Security=True";
            sqltext = "Select [ObservationPosts].[ID], [ObservationPosts].[NameOfPost],";
            sqltext += "[Streets].[Street], [ObservationPosts].[NumberOfHouse]";
            sqltext += "from [ObservationPosts]";
            sqltext += "inner join [Streets] on [ObservationPosts].[Street] = [Streets].[ID]";
            Form18 f = new Form18();
            SqlDataAdapter da = new SqlDataAdapter(sqltext, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "[ObservationPosts]");
            f.dataGridView1.DataSource = ds.Tables["[ObservationPosts]"].DefaultView;
            f.dataGridView1.Columns[1].Width = 200;
            f.dataGridView1.Columns[2].Width = 150;
            f.Show();
        }

        private void улицыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqltext;
            string ConnStr = @"Data Source=DESKTOP-U184JJA\SQLEXPRESS;Initial Catalog=Pollution;Integrated Security=True";
            sqltext = "Select [Streets].[ID], [Streets].[Street], [Districts].[District]";
            sqltext += "from [Streets]";
            sqltext += "inner join [Districts] on [Streets].[District] = [Districts].[ID]";
            Form19 f = new Form19();
            SqlDataAdapter da = new SqlDataAdapter(sqltext, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "[Streets]");
            f.dataGridView1.DataSource = ds.Tables["[Streets]"].DefaultView;
            f.dataGridView1.Columns[1].Width = 200;
            f.dataGridView1.Columns[2].Width = 150;
            f.Show();
        }

        private void районыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqltext;
            string ConnStr = @"Data Source=DESKTOP-U184JJA\SQLEXPRESS;Initial Catalog=Pollution;Integrated Security=True";
            sqltext = "Select * From [Districts]";
            Form20 f = new Form20();
            SqlDataAdapter da = new SqlDataAdapter(sqltext, ConnStr);
            DataSet ds = new DataSet();
            da.Fill(ds, "[Districts]");
            f.dataGridView1.DataSource = ds.Tables["[Districts]"].DefaultView;
            f.dataGridView1.Columns[1].Width = 150;
            f.Show();
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void улицыИРайоныToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
