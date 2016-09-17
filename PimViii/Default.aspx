<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PimViii.Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" ValidateRequestMode="Inherit">
    <div class="panel panel-default">
        <div class="alert alert-success alert-dismissible" role="alert" runat="server" id="alertaSucesso" visible="false">
          <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
          Atividade cadastrada com sucesso!
        </div>
        <div class="panel-heading">Cadastro de Atividades</div>
        <div class="panel-body">
            <div class="jumbotron col-md-12">
                <table class="table-hover col-md-10">
                    <tr>
                    <th><asp:Label ID="Label1" runat="server" Text="Tipo de Tarefa"></asp:Label></th>
                        </tr>
                    <tr>
                    <td><asp:DropDownList ID="DropDownList1" runat="server" CssClass="dropdown form-control">
                            <asp:ListItem>Trabalhos</asp:ListItem>
                            <asp:ListItem>Provas</asp:ListItem>
                            <asp:ListItem>AC</asp:ListItem>
                            <asp:ListItem>DPs</asp:ListItem>
                            <asp:ListItem>Outros</asp:ListItem>
                        </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td><span class="label label-danger" runat="server" ID="avisoDiscriminacao" visible="false">Campo Obrigatório</span></td>
                        </tr>
                    <tr>
                        <td><asp:TextBox ID="txtDiscriminacao" placeholder="Discriminação" runat="server" MaxLength="30" CssClass="form-control"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td></td>
                        </tr>
                    <tr>
                        <td><asp:TextBox ID="txtDetalhes" placeholder="Detalhes" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th><asp:Label ID="Label3" runat="server" Text="Data limite"></asp:Label></th>
                        </tr>
                    <tr>
                        <td><asp:Calendar ID="Calendar1" runat="server" OnDayRender="Calendar1_DayRender" CssClass="table-bordered table-condensed table-striped table-hover"></asp:Calendar></td>
                    </tr>
                    <tr><td></td></tr>
                    <tr>
                        <td class="text-center"><asp:Button ID="btnCadastrar" runat="server" Text="Cadastrar" CssClass ="btn btn-lg btn-primary" OnClick="btnCadastrar_Click"/>
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass ="btn btn-lg btn-danger" OnClick="btnCancelar_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>       
    </div>
 </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SecundaryContent" runat="server">
    <div class="panel panel-default">
        <div class="alert alert-warning alert-dismissible" role="alert" runat="server" id="alertaPrazo" visible="false">
            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            <strong>Atenção!</strong> Existem Atividades que finalizam hoje.
        </div>
        <div class="alert alert-danger alert-dismissible" role="alert" runat="server" id="alertaForaPrazo" visible="false">
            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            <strong>Atenção!</strong> Existem Atividades com prazo encerrado.
        </div>
        <div class="alert alert-success alert-dismissible" role="alert" runat="server" id="alertaSucesso2" visible="false">
            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            Operação executada com sucesso!
        </div>
         <div class="alert alert-danger alert-dismissible" role="alert" runat="server" id="alertaErroData" visible="false">
            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            <strong>Atenção!</strong> Data inválida!
        </div>
        <div class="panel-heading">Atividades Acadêmicas</div>
        <div class="panel-body">
            <div class="jumbotron">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" DataKeyNames ="Id" CssClass="table table-striped">
                    <Columns>
                        <asp:TemplateField HeaderText="Id">
                            <EditItemTemplate>
                                <asp:Label ID="lblid" runat="server" Text ='<%# Bind("Id") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblid" runat="server" Text ='<%# Bind("Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipo">
                            <EditItemTemplate>
                                        <asp:DropDownList ID="DropDownList2" runat="server">
                                            <asp:ListItem>Trabalhos</asp:ListItem>
                                            <asp:ListItem>Provas</asp:ListItem>
                                            <asp:ListItem>AC</asp:ListItem>
                                            <asp:ListItem>DPs</asp:ListItem>
                                            <asp:ListItem>Outros</asp:ListItem>
                                        </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblTipo" runat="server" Text ='<%# Bind("Tipo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Discriminação">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDiscriminacao" runat="server" Text ='<%# Bind("Discriminacao") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDiscriminacao" runat="server" Text ='<%# Bind("Discriminacao") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Detalhes">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDetalhes" runat="server" Text ='<%# Bind("Detalhes") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDetalhes" runat="server" Text ='<%# Bind("Detalhes") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Data Limite">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDataLimite" runat="server" Text ='<%# Bind("DataLimite", "{0:d}") %>'></asp:TextBox>
                                <asp:CalendarExtender ID="Calendar2" runat="server" TargetControlID="txtDataLimite"></asp:CalendarExtender>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDataLimite" runat="server" Text ='<%# Bind("DataLimite", "{0:d}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CommandName="Update" CssClass ="btn btn-xs btn-primary"/>
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CommandName="Cancel" CssClass ="btn btn-xs btn-danger"/>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandName="Edit" CssClass ="btn btn-xs btn-primary"/>
                                <asp:Button ID="btnExcluir" runat="server" Text="Excluir" CommandName="Delete" CssClass ="btn btn-xs btn-danger"/>
                            </ItemTemplate>
                        </asp:TemplateField>           
                    </Columns>
                </asp:GridView>
                <asp:Label ID="Label5" runat="server" Text="Observação: Marcação em amarelo identifica atividade com prazo até hoje."></asp:Label>
                <br />
                <asp:Label ID="Label6" runat="server" Text="Observação: Marcação em vermelho identifica atividade com prazo vencido."></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
