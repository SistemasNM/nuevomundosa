Public Class Lote
#Region "   Variables"
    Private mstrCodigoLote As String
    Private mstrUsuario As String
    Private mdsSet As DataSet
    Private mbooOk As Boolean
#End Region
#Region "   Propiedades"
    Public ReadOnly Property Estado() As Boolean
        Get
            Estado = mbooOk
        End Get
    End Property
    Public ReadOnly Property SetDatos() As DataSet
        Get
            SetDatos = mdsSet
        End Get
    End Property
    Public Property CodigoLote() As String
        Get
            CodigoLote = mstrCodigoLote
        End Get
        Set(ByVal Value As String)
            mstrCodigoLote = Value
        End Set
    End Property
#End Region
#Region "   Metodos"
    Sub New(ByVal strUsuario As String)
        mbooOk = False
        mstrUsuario = strUsuario
    End Sub

    Public Function Buscar() As Boolean
        Dim lobjTinto As New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        Dim larrParametros() As String = {"var_CodigoLote", mstrCodigoLote} '"P20070526-00009"}
        mbooOk = False
        Try
            mdsSet = lobjTinto.ObtenerDataSet("usp_ALM_Lote_BuscarPartidasXDespachar", larrParametros)
            mdsSet.Tables(0).TableName = "Cabecera"
            mdsSet.Tables(1).TableName = "ParaDespacho"
            mdsSet.Tables(2).TableName = "Aplicaciones"
            mdsSet.Tables(3).TableName = "Tinas"
            mdsSet.Tables(4).TableName = "Partidas"
            mdsSet.Tables(5).TableName = "Insumos"
            mdsSet.Tables(6).TableName = "Consumos"
            mdsSet = FormatearSet(mdsSet)
            mbooOk = True
        Catch ex As Exception
            mdsSet = Nothing
            mbooOk = False
        Finally
            lobjTinto = Nothing
        End Try
        Return mbooOk
    End Function

    Private Function FormatearSet(ByVal dsSetDatos As DataSet) As DataSet
        Dim ldrFila As DataRow
        Dim ldrNuevaFila As DataRow
        Dim ldrCalculo As DataRow
        Dim ldtConsumo As New DataTable("Consumo")
        Dim ldtPorDespachar As DataTable = dsSetDatos.Tables("ParaDespacho")
        Dim i As Integer
        Dim j As Integer
        Dim ldvVista As DataView
        Dim lstrFiltro As String = ""
        Dim larrPartes() As String
        Dim lstrAplicacion As String
        Dim lstrPartida As String
        Dim larrLlave() As DataColumn
        Dim larrFiltro(1) As String
        Dim ldblTotal As Double
        Dim ldsSetDatos As New DataSet("Lote")
        Dim lintMinimo As Integer



        ReDim larrLlave(1)
        larrLlave(0) = ldtPorDespachar.Columns("tipo_aplicacion")
        larrLlave(1) = ldtPorDespachar.Columns("secuencia_partida")
        ldtPorDespachar.PrimaryKey = larrLlave

        For Each ldrFila In ldtPorDespachar.Select("", "tipo_aplicacion,secuencia_partida")
            lintMinimo = (10 * (ldrFila("tipo_aplicacion") + 1)) + ldrFila("secuencia_partida")
            Exit For
        Next
        'Creando estructura de consumo
        ldtConsumo.Columns.Add("codigo_insumo", GetType(String))
        ldtConsumo.Columns.Add("nombre_insumo", GetType(String))
        For Each ldrFila In dsSetDatos.Tables("Partidas").Select("", "tipo_aplicacion,secuencia_partida")
            If (10 * (ldrFila("tipo_aplicacion") + 1)) + ldrFila("secuencia_partida") >= lintMinimo Then
                ldtConsumo.Columns.Add(CStr(ldrFila("tipo_aplicacion") & "-" & CStr(ldrFila("secuencia_partida"))), GetType(Double))
            End If
        Next
        'Agregando los diferentes insumos quimicos(no consumos)
        ldtConsumo.Columns.Add("Total", GetType(Double))
        For Each ldrFila In dsSetDatos.Tables("Insumos").Copy.Select("", "orden1,orden2")
            ldblTotal = 0
            ldrNuevaFila = ldtConsumo.NewRow
            ldrNuevaFila.BeginEdit()
            ldrNuevaFila.Item("codigo_insumo") = ldrFila.Item("codigo_insumo")
            ldrNuevaFila.Item("nombre_insumo") = ldrFila.Item("nombre_insumo")
            For i = 2 To ldtConsumo.Columns.Count - 2
                larrPartes = Split(ldtConsumo.Columns(i).ColumnName, "-")
                lstrAplicacion = larrPartes(0)
                lstrPartida = larrPartes(1)
                larrFiltro(0) = lstrAplicacion
                larrFiltro(1) = lstrPartida
                lstrFiltro = "codigo_insumo='" + ldrFila.Item("codigo_insumo") + "' and tipo_aplicacion=" + lstrAplicacion + " and secuencia_partida =" + lstrPartida
                ldrNuevaFila.Item(i) = 0
                ldvVista = New DataView(SetDatos.Tables("Consumos"), lstrFiltro, "", DataViewRowState.CurrentRows)
                For j = 0 To ldvVista.Count - 1
                    ldrNuevaFila.Item(i) = ldrNuevaFila.Item(i) + ldvVista.Item(j).Item("cantidad")
                Next
                ldrNuevaFila.Item(i) = Math.Round(ldrNuevaFila.Item(i), 4)
                If Not ldtPorDespachar.Rows.Find(larrFiltro) Is Nothing Then
                    ldblTotal = ldblTotal + ldrNuevaFila.Item(i)
                End If
            Next i
            ldrNuevaFila.Item("Total") = Math.Round(ldblTotal, IIf(Left(ldrFila.Item("codigo_insumo"), 4) = "0302", 4, 1))
            ldrNuevaFila.EndEdit()
            ldtConsumo.Rows.Add(ldrNuevaFila)
        Next
        ldsSetDatos.Tables.Add(ldtConsumo)
        ldsSetDatos.Tables.Add(dsSetDatos.Tables("Aplicaciones").Copy)
        ldsSetDatos.Tables.Add(dsSetDatos.Tables("Tinas").Copy)
        ldsSetDatos.Tables.Add(dsSetDatos.Tables("Partidas").Copy)
        ldsSetDatos.Tables.Add(dsSetDatos.Tables("ParaDespacho").Copy)
        Return ldsSetDatos
    End Function
#End Region
End Class
