Imports NM.AccesoDatos

Namespace Maestros
  Public Class Ruta
    Private mdtDetalle As DataTable
    Private mstrUsuario As String
    Private mintRevision As Integer
    Private mstrCodigoArticulo As String
    Private mstrCodigoArticuloLargo As String
    Private mstrCodigoArticuloOFISIS As String
    Private mintTipoLista As Int16 = 0

    Sub New(ByVal strUsuario As String, ByVal strCodigoArticulo As String)
      mstrUsuario = strUsuario
      CodigoArticulo = strCodigoArticulo
    End Sub

    Public Property Revision() As Integer
      Get
        Revision = mintRevision
      End Get
      Set(ByVal Value As Integer)
        mintRevision = Value
      End Set
    End Property
    Public Property Detalle() As DataTable
      Get
        Detalle = mdtDetalle
      End Get
      Set(ByVal value As DataTable)
        Dim larrKey(2) As DataColumn
        larrKey(0) = value.Columns("codigo_articulo_ofisis")
        larrKey(1) = value.Columns("codigo_articulo_largo")
        larrKey(2) = value.Columns("secuencial")
        value.PrimaryKey = larrKey
        mdtDetalle = value
        If mdtDetalle.Rows.Count > 0 Then
          mintRevision = mdtDetalle.Rows(0).Item("revision_ruta")
        End If
      End Set
    End Property
    Public WriteOnly Property CodigoArticulo() As String
      Set(ByVal Value As String)
        Select Case Len(Value)
          Case 30
            mstrCodigoArticuloOFISIS = Value
          Case 20
            mstrCodigoArticuloLargo = Value
        End Select
      End Set
    End Property

    Public Property TipoLista() As Int16
      Get
        TipoLista = mintTipoLista
      End Get
      Set(ByVal Value As Int16)
        mintTipoLista = Value
      End Set
    End Property

    Public Function Subprocesos() As DataTable
      Dim ldtSubprocesos As New DataTable("SubProcesos")
      Dim larrKey(0) As DataColumn
      Dim ldrFila As DataRow
      Dim ldrNuevaFila As DataRow
      ldtSubprocesos.Columns.Add("codigo_subproceso", GetType(String))
      ldtSubprocesos.Columns.Add("codigo_etapa", GetType(String))
      ldtSubprocesos.Columns.Add("descripcion_etapa", GetType(String))
      ldtSubprocesos.Columns.Add("revision_subproceso", GetType(String))
      ldtSubprocesos.Columns.Add("descripcion_subproceso", GetType(String))
      larrKey(0) = ldtSubprocesos.Columns("codigo_subproceso")
      ldtSubprocesos.PrimaryKey = larrKey
      For Each ldrFila In mdtDetalle.Rows
        ldrNuevaFila = ldtSubprocesos.NewRow
        ldrNuevaFila.Item("codigo_etapa") = ldrFila.Item("codigo_etapa")
        ldrNuevaFila.Item("descripcion_etapa") = ldrFila.Item("descripcion_etapa")
        ldrNuevaFila.Item("codigo_subproceso") = ldrFila.Item("codigo_subproceso")
        ldrNuevaFila.Item("descripcion_subproceso") = ldrFila.Item("descripcion_subproceso")
        ldrNuevaFila.Item("codigo_subproceso") = ldrFila.Item("codigo_subproceso")
        ldtSubprocesos.LoadDataRow(ldrNuevaFila.ItemArray, True)
      Next ldrFila
      ldrFila = Nothing
      ldrNuevaFila = Nothing
      Return ldtSubprocesos
    End Function
    Public Function Buscar() As DataTable
      Dim lobjTinto As AccesoDatosSQLServer
      Dim lstrParametros() As String = { _
        "tin_TipoLista", mintTipoLista, _
        "var_CodigoArticuloLargo", mstrCodigoArticuloLargo}
      Try
        lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        mdtDetalle = lobjTinto.ObtenerDataTable("usp_TIN_Articulo_ObtenerRuta", lstrParametros)
      Catch ex As Exception
        Throw ex
      Finally
        lobjTinto = Nothing
      End Try
    End Function
    Public Function DetalleSubproceso(ByVal strCodigoSubProceso As String) As DataTable
      Dim ldtSPDetalle As New DataTable("SubProceso_Detalle")
      Dim larrKey(3) As DataColumn
      Dim ldrFila As DataRow
      Dim ldrNuevaFila As DataRow

      With ldtSPDetalle.Columns
        .Add("codigo_articulo", GetType(String))
        .Add("revision_ruta", GetType(Integer))
        .Add("codigo_subproceso", GetType(String))
        .Add("revision_subproceso", GetType(Integer))
        .Add("descripcion_subproceso", GetType(String))
        .Add("secuencial", GetType(Integer))
        .Add("secuencial_subproceso", GetType(Integer))
        .Add("posicion", GetType(Integer))
        .Add("codigo_maquina", GetType(String))
        .Add("revision_maquina", GetType(Integer))
        .Add("descripcion_maquina", GetType(String))
        .Add("codigo_operacion", GetType(String))
        .Add("descripcion_operacion", GetType(String))
        .Add("codigo_receta", GetType(String))
        .Add("revision_receta", GetType(Integer))
        '.Add("descripcion_receta", GetType(String))
        .Add("velocidad", GetType(Double))
        .Add("pases", GetType(Integer))
        .Add("estado", GetType(Integer))

        larrKey(0) = .Item("codigo_articulo")
        larrKey(1) = .Item("codigo_subproceso")
        larrKey(2) = .Item("secuencial_subproceso")
      End With
      ldtSPDetalle.PrimaryKey = larrKey
      For Each ldrFila In mdtDetalle.Select("codigo_subproceso='" + strCodigoSubProceso + "'", "secuencial_subproceso")
        ldrNuevaFila = ldtSPDetalle.NewRow
        With ldrNuevaFila
          .Item("codigo_articulo") = ldrFila.Item("codigo_articulo_largo")
          .Item("revision_ruta") = ldrFila.Item("revision_ruta")
          .Item("codigo_subproceso") = ldrFila.Item("codigo_subproceso")
          .Item("revision_subproceso") = ldrFila.Item("revision_subproceso")
          .Item("descripcion_subproceso") = ldrFila.Item("descripcion_subproceso")
          .Item("secuencial") = ldrFila.Item("secuencial")
          .Item("secuencial_subproceso") = ldrFila.Item("secuencial_subproceso")
          .Item("posicion") = ldrFila.Item("posicion")
          .Item("codigo_maquina") = ldrFila.Item("codigo_maquina")
          .Item("revision_maquina") = ldrFila.Item("revision_maquina")
          .Item("descripcion_maquina") = ldrFila.Item("descripcion_maquina")
          .Item("codigo_operacion") = ldrFila.Item("codigo_operacion")
          .Item("descripcion_operacion") = ldrFila.Item("descripcion_operacion")
          .Item("codigo_receta") = ldrFila.Item("codigo_receta")
          .Item("revision_receta") = ldrFila.Item("revision_receta")
          '.Item("descripcion_receta") = ldrFila.Item("descripcion_receta")
          .Item("velocidad") = ldrFila.Item("velocidad")
          .Item("pases") = ldrFila.Item("pases")
          .Item("Estado") = ldrFila.Item("Estado")
          ldtSPDetalle.Rows.Add(ldrNuevaFila)
        End With
      Next
      ldrNuevaFila = Nothing
      ldrFila = Nothing
      Return ldtSPDetalle
    End Function
  End Class
End Namespace

