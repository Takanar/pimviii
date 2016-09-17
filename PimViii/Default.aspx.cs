using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PimViii
{
    public partial class Default : System.Web.UI.Page
    {
        string tarefa, discriminacao, detalhes;
        DateTime data;
        Models.Crud crud = new Models.Crud();

        protected void Page_Load(object sender, EventArgs e)
        {
              
            alertaErroData.Visible = false;
            alertaSucesso.Visible = false;
            alertaSucesso2.Visible = false;
            avisoDiscriminacao.Visible = false;
            //contorna erro PostBack
            if (!IsPostBack)             
            {
                CarregarTabela();
                VerificaDataAnterior();
                VerificaData();
            }
        }
        //desabilita dias anteriores
        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e) 
        {
            if (e.Day.Date < DateTime.Today)
            {
                e.Day.IsSelectable = false;
            }
        }

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(txtDiscriminacao.Text) != "")
            {
                tarefa = DropDownList1.SelectedValue;
                discriminacao = txtDiscriminacao.Text;
                detalhes = txtDetalhes.Text;
                if (Calendar1.SelectedDate == new DateTime(0001, 1, 1, 0, 0, 0)) //verifica se data não foi selecionada
                {
                    data = Calendar1.TodaysDate;                                // atribui data de hoje
                }
                else data = Calendar1.SelectedDate;
                crud.Create(tarefa, discriminacao, detalhes, data);
                alertaSucesso.Visible = true;
                CarregarTabela();
                VerificaData();
                VerificaDataAnterior();
                txtDiscriminacao.Text = "";
                txtDetalhes.Text = "";
            }
            else avisoDiscriminacao.Visible = true;            
        }
        //Carrega dados do BD
        public void CarregarTabela()
        {
            GridView1.DataSource = crud.Read();
            GridView1.DataMember = crud.Read().Tables[0].TableName;
            GridView1.Columns[0].Visible = false;
            GridView1.DataBind();
        }

        //Verifica se há atividades com prazo até esta data
        public void VerificaData()
        {
            int retorno = crud.VerificaData();
            if (retorno == 0)
            {
                alertaPrazo.Visible = true;
                ScriptManager.RegisterClientScriptBlock(Page,Page.GetType(),"mensagem", "<script type=\"text/javascript\"> alert('Atenção! Existem atividades que finalizam hoje!');</script>", false);
            }
        }
        // Marca atividades que já venceram.
        public void VerificaDataAnterior()
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Label txtDataLimite = GridView1.Rows[i].FindControl("lblDataLimite") as Label;
                DateTime dataHj = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                DateTime dataBd = Convert.ToDateTime(txtDataLimite.Text);
                int j = DateTime.Compare(dataHj, dataBd);
                if (j>0)    
                {
                    GridView1.Rows[i].CssClass = "danger";
                    alertaForaPrazo.Visible = true;
                }
                if (j == 0)
                {
                    GridView1.Rows[i].CssClass = "warning";
                }
            }
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            CarregarTabela();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int index = e.RowIndex;
            Label txtId = GridView1.Rows[index].FindControl("lblid") as Label;
            DropDownList DropDownList2 = GridView1.Rows[index].FindControl("DropDownList2") as DropDownList;
            TextBox txtDiscriminacao = GridView1.Rows[index].FindControl("txtDiscriminacao") as TextBox;
            TextBox txtDetalhes = GridView1.Rows[index].FindControl("txtDetalhes") as TextBox;
            TextBox txtDataLimite = GridView1.Rows[index].FindControl("txtDataLimite") as TextBox;
            int Id = Convert.ToInt32(txtId.Text);
            string tipo = DropDownList2.SelectedValue;
            string discriminacao = (txtDiscriminacao).Text;
            string detalhes = (txtDetalhes).Text;
            DateTime dataLimite = Convert.ToDateTime((txtDataLimite).Text);
            DateTime dataHoje = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            int j = DateTime.Compare(dataHoje, dataLimite);
            if (j <= 0)
            {
                crud.Update(Id, tipo, discriminacao, detalhes, dataLimite);
                alertaSucesso2.Visible = true;
                GridView1.EditIndex = -1;
                CarregarTabela();
                VerificaData();
                VerificaDataAnterior();
            }
            if (j>0)
                alertaErroData.Visible = true;
                CarregarTabela();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            crud.ConectarBd();
            crud.Delete(id);
            alertaSucesso2.Visible = true;
            CarregarTabela();
            VerificaData();
            VerificaDataAnterior();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            txtDiscriminacao.Text = "";
            txtDetalhes.Text = "";
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            CarregarTabela();
            VerificaData();
            VerificaDataAnterior();
        }
    }
}