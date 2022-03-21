using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ExcelDataReader;


namespace TransportOrder
{
    public partial class Main_window : Form
    
    {   
        // Переменные названия новых колонок
        const string colomnNameStartDayCalculate = "Начало расчета";
        const string colomnNameFinishDayCalculate = "Конец расчета";
        const string colomnNameDaysCount = "Кол-во дней хранения";
        const string colomnNamePrise = "Ставка";
        const string colomnNameComment = "Примечание";


        DataSet dataSet; //Дата сэт полученный из эксель файла

        public Main_window()
        {
            InitializeComponent();
        }
        

        /// <summary>
        /// Обработчик нажатия на кнопку загрузки файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {

                //Настройка окна для загрузки файлов
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Excel files(*.xlsx)|*.xlsx|All files (*.*)|*.*";
               
                //При подтверждении выбора файла
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    OpenExcelFile(openFileDialog.FileName);

                }
            }
        }

        /// <summary>
        /// Метод для загрузки данных из файла
        /// </summary>
        /// <param name="filePath"></param>
        private void OpenExcelFile(string filePath)
        {
            //Создание потока файлов
            FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            //Создание объекта для чтения файла
            IExcelDataReader dataReader = ExcelReaderFactory.CreateReader(stream);

            dataSet = dataReader.AsDataSet(new ExcelDataSetConfiguration()
            {//Необходимо для чтения названия колонок из эксель 
                ConfigureDataTable = (x) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            }
            );

            dataDisplayGrid.DataSource = dataSet.Tables[0];
        }

        /// <summary>
        /// Обработчик нажатия на кнопку Рассчитать
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            DataTable result_table = calculateCargo();
            dataDisplayGrid.DataSource = result_table;
        }

        /// <summary>
        /// Получение готовой таблицы и подсчета всех данных
        /// </summary>
        /// <returns></returns>
        private DataTable calculateCargo()
        {
            DataTable dataForDisplay = dataSet.Tables[0]; //Таблицыа для отображения "Груз"
            DataTable dataForCalculating = dataSet.Tables[0]; //Таблица для оценки стоимости "Тариф"
            
            addColumnsInTable(ref dataForDisplay);

            FillTable(ref dataForDisplay);

            return dataForDisplay;
        }
        /// <summary>
        /// Добавление новых колонок в таблицу
        /// </summary>
        /// <param name="data">DataTable таблица для отображения</param>
        private void addColumnsInTable(ref DataTable data)
        {
            data.Columns.Add(new DataColumn(colomnNameStartDayCalculate));
            data.Columns.Add(new DataColumn(colomnNameFinishDayCalculate));
            data.Columns.Add(new DataColumn(colomnNameDaysCount));
            data.Columns.Add(new DataColumn(colomnNamePrise));
            data.Columns.Add(new DataColumn(colomnNameComment));
        }

        private void FillTable(ref DataTable data)
        { int i, j;
            for (i = 0; i < data.Rows.Count; i++)
            {
                    DataRow newRow = MakeRowToPaste(data.Rows[i]);   
            }
        }

        private DataRow MakeRowToPaste(DataRow dataRow)
        {
            PasteStartDay(ref dataRow);
            Console.Write(Convert.ToString(dataRow));
            PasteFinishDay(ref dataRow);
            return dataRow;
        }

        //Подстановка актуальной даты начала рассчета
        private void PasteStartDay(ref DataRow dataRow)
        {
            
        }

        //Подстановка, если возможно актаульной даты окончания рассчета
        private void PasteFinishDay(ref DataRow dataRow)
        {

        }
    }
    
}
