Option Strict On

Imports System.Data
Imports System.Xml

Namespace NM.AccesoDatos
    Public MustInherit Class AccesoDatosGenerico
        Protected m_strCadenaConexion As String

        Public Sub New(ByVal BaseDatos As GeneradorCadenaConexion.enmBasesDatos)
            m_strCadenaConexion = GeneradorCadenaConexion.ObtenerCadenaConexionSQLServer(BaseDatos)
        End Sub

        Protected MustOverride Function ConfigurarParametros(ByVal objParametros As Object()) As Object()

        Public MustOverride Overloads Function EjecutarComando(ByVal strProcedimiento As String) As Integer
        Public MustOverride Overloads Function EjecutarComando(ByVal strProcedimiento As String, ByVal objParametros As Object()) As Integer

        Public MustOverride Overloads Function ObtenerDataSet(ByVal strProcedimiento As String) As DataSet
        Public MustOverride Overloads Function ObtenerDataSet(ByVal strProcedimiento As String, ByVal objParametros As Object()) As DataSet

        Public MustOverride Overloads Function ObtenerDataTable(ByVal strProcedimiento As String) As DataTable
        Public MustOverride Overloads Function ObtenerDataTable(ByVal strProcedimiento As String, ByVal objParametros As Object()) As DataTable

        Public MustOverride Overloads Function ObtenerDataReader(ByVal strProcedimiento As String) As IDataReader
        Public MustOverride Overloads Function ObtenerDataReader(ByVal strProcedimiento As String, ByVal objParametros As Object()) As IDataReader

        Public MustOverride Overloads Function ObtenerXmlReader(ByVal strProcedimiento As String) As XmlReader
        Public MustOverride Overloads Function ObtenerXmlReader(ByVal strProcedimiento As String, ByVal objParametros As Object()) As XmlReader

        Public MustOverride Overloads Function ObtenerValor(ByVal strProcedimiento As String) As Object
        Public MustOverride Overloads Function ObtenerValor(ByVal strProcedimiento As String, ByVal objParametros As Object()) As Object
    End Class
End Namespace