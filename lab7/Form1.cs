namespace lab7
{
    public partial class Form1 : Form
    {
        private HospitalQueue _hospitalQueue;
        private Serialization _serialization;

        public Form1()
        {
            InitializeComponent();
            _hospitalQueue = new HospitalQueue();
            _serialization = new Serialization();

        }

        private void btShow_Click(object sender, EventArgs e)
        {
            UpdateQueueListBox();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            string firstName = TBFirstName.Text;
            string lastName = TBLastName.Text;

            //�������� �� ���������� ��� �����
            if (int.TryParse(TBAge.Text, out int age) && int.TryParse(TBId.Text, out int id))
            {
                Patient patient = new Patient(firstName, lastName, age, id);
                _hospitalQueue.AddPatient(patient);
                UpdateQueueListBox(); //��������� ������ �������
            }
            else
            {
                MessageBox.Show("������������ ������ ��������� ������.", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // ������ ��������� �����
            TBFirstName.Clear();
            TBLastName.Clear();
            TBAge.Clear();
            TBId.Clear();
        }

        private void btRemove_Click(object sender, EventArgs e)
        {

            _hospitalQueue.RemoveFirstPatient(); 
            UpdateQueueListBox();
            
        }
        private void UpdateQueueListBox()
        {
            listBoxQuque.Items.Clear();
            string queueInfo = _hospitalQueue.DisplayQueue();
            // ���������� ������ queueInfo �� ������ �������� ��� ��������� � ����������� ������� � ��������
            string[] patientInfoArray = queueInfo.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            // ��������� ���������� � ��������� � ������ �������
            foreach (string patientInfo in patientInfoArray)
            {
                listBoxQuque.Items.Add(patientInfo);
            }
        }

        private void SerializeToJsonBtn_Click(object sender, EventArgs e)
        {
            string filePath = "hospitalQueue.json";
            _serialization.SerializeToJson(_hospitalQueue._patientsQueue, filePath);

        }

        private void DeserializeFromJsonBtn_Click(object sender, EventArgs e)
        {
            string filePath = "hospitalQueue.json";
            HospitalQueue deserializedQueue = Serialization.DeserializeFromJson(filePath);
            _hospitalQueue = deserializedQueue;
            UpdateQueueListBox();
        }

        private void SerializeToBinaryBtn_Click(object sender, EventArgs e)
        {
            string filePath = "hospitalQueue.bin";
            _serialization.SerializeToBinary(_hospitalQueue._patientsQueue, filePath);
        }

        private void DeserializeFromBin_Click(object sender, EventArgs e)
        {
            string filePath = "hospitalQueue.bin";
            HospitalQueue loadedQueue = Serialization.DeserializeFromBinary(filePath);
            _hospitalQueue = loadedQueue;
            UpdateQueueListBox();
        }
    }
}