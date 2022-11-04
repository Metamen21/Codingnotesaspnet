Imports System.Collections.Generic

Partial Class VB
    Inherits System.Web.UI.Page
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetCountries() As List(Of Country)
        Dim countries As List(Of Country) = New List(Of Country)

        Dim country As New Country
        country.Name = "India"
        country.Id = "1"
        countries.Add(country)

        country = New Country
        country.Name = "USA"
        country.Id = "2"
        countries.Add(country)

        country = New Country
        country.Name = "Canada"
        country.Id = "3"
        countries.Add(country)

        Return countries
    End Function
End Class
Public Class Country
    Private _name As String
    Public Property Name As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Private _id As String
    Public Property Id As String
        Get
            Return _id
        End Get
        Set(ByVal value As String)
            _id = value
        End Set
    End Property
End Class

