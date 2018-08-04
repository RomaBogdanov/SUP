using System;
using System.Windows.Forms;

namespace AndoverPersonsManager
{
    public partial class PersonForm : Form
    {
        private readonly ProgramData _programData;
        private PersonCore _person;

        public PersonForm(ProgramData programData, PersonCore person)
        {
            _programData = programData;
            InitializeComponent();

            _person = person ?? new PersonCore();

            if (person != null)
            {
                textBoxFirstName.Text = _person.LastName;
                textBoxFirstName.ReadOnly = true;
                textBoxLastName.Text = _person.FirstName;
                textBoxLastName.ReadOnly = true;
                textBoxCard.Text = _person.CardNumber;
                textBoxPath.Text = _person.Path;
                textBoxPath.ReadOnly = true;
            }
        }

        public PersonCore Person { get { return _person; } }

        private void buttonZones_Click(object sender, EventArgs e)
        {
            var form = new ZonesForm(_programData, _person);
            if (form.ShowDialog() == DialogResult.OK)
            {
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            _person = _person ?? new PersonCore();

            _person.LastName = textBoxFirstName.Text;
            _person.FirstName = textBoxLastName.Text;
            _person.CardNumber = textBoxCard.Text;
            _person.Path = textBoxPath.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
