<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="RezervacijaSC.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        Veb aplikacija treba da ima sledeću funkcionalnost: pri startovanju aplikacije slobodna
sedišta su obeležena zelenom bojom a rezervisana crvenom. Klikom miša na slobodno
sedište u polje forme Broj sedišta automatski se unosi podatak o sedištu koje želimo da
rezervišemo. Kada se forma popuni ostalim podacima i pošalje, slobodno sedište menja
boju u crvenu, to jest postaje rezervisano i u bazi se menja polje rezervacija. 
    </div>
</asp:Content>
