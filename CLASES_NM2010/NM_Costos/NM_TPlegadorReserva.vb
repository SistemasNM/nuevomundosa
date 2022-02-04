Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Tejeduria
  Public Class NM_TPlegadorReserva

    '---Atributos
    Public Fecha As Date
    Public CentroCosto As String
    Public codigo_partida As String
    Public NumPlegador As String
    Public Ubicacion As String
    Public Usuario As String
    'Private objUtil As New NM_Produccion.NM_Util.NM_Util
    Private objUtil As New NM_General.Util
    Private lobj_Conexion As AccesoDatosSQLServer

    Public Sub New()
      Fecha = Date.Today
      CentroCosto = ""
      NumPlegador = ""
      Ubicacion = ""
      Usuario = ""

      lobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
    End Sub

    '----Metodos
    Public Function insertar() As Boolean
      'Dim objConn As New NM_Consulta
      'Dim strsql As String = "INSERT INTO NM_PlegadoresReserva " & _
      '"(fecha, centro_costo, codigo_partida, numero_plegador, ubicacion, usuario_creacion, fecha_creacion) " & _
      '"values(convert(datetime, '" & objUtil.FormatFecha(Fecha) & "'),'" & CentroCosto & _
      '"', '" & codigo_partida & "','" & NumPlegador & "','" & Ubicacion & "','" & Usuario & "',getdate())"

      'Return objConn.Execute(strsql)

      Dim ldtb_Datos As New DataTable, ldtr_row As DataRow
      Dim lobj_xml As New NM_General.Util, lstr_mensaje As String

      'p1 as [Fecha],
      'p2 as [CentroCosto],
      'p3 as [codigo_partida],
      'p4 as [NumPlegador],
      'p5 as [Ubicacion],
      'p6 as [Usuario]

      ldtb_Datos.Columns.Add("p1", GetType(String))
      ldtb_Datos.Columns.Add("p2", GetType(String))
      ldtb_Datos.Columns.Add("p3", GetType(String))
      ldtb_Datos.Columns.Add("p4", GetType(String))
      ldtb_Datos.Columns.Add("p5", GetType(String))
      ldtb_Datos.Columns.Add("p6", GetType(String))
      ldtb_Datos.TableName = "lista"


      ldtr_row = ldtb_Datos.NewRow
      ldtr_row("p1") = Fecha.Year.ToString & Right("0" & Fecha.Month.ToString, 2) & Right("0" & Fecha.Day.ToString, 2) 'YYYYMMDD
      ldtr_row("p2") = CentroCosto
      ldtr_row("p3") = codigo_partida
      ldtr_row("p4") = NumPlegador
      ldtr_row("p5") = Ubicacion
      ldtr_row("p6") = Usuario
      ldtb_Datos.Rows.Add(ldtr_row)

      lstr_mensaje = ""

      Dim lobj_parametros() As Object = {"pchr_accion", "INS", _
                   "pnvc_datos", lobj_xml.GeneraXml(ldtb_Datos), _
                   "pvch_mensaje", lstr_mensaje}

      lobj_Conexion.EjecutarComando("usp_tej_plegadoresreserva_guardar", lobj_parametros)


    End Function
    Public Function Eliminar(ByVal pNumPlegador As String, ByVal dFecha As Date, ByVal pCentroCosto As String) As Boolean
      Dim strsql As String, objConn As New NM_Consulta
      strsql = "DELETE FROM NM_PlegadoresReserva " & _
      " WHERE numero_plegador ='" & pNumPlegador & _
      "' and DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(dFecha) & "') = 0 and centro_costo = '" & pCentroCosto & "' "
      Return objConn.Execute(strsql)
    End Function
    Public Function Exist(ByVal pNumPlegador As String, ByVal dFecha As Date, ByVal pCentroCosto As String) As Boolean
      Dim strsql As String, dt As New DataTable
      Dim objConn As New NM_Consulta
      strsql = "Select * FROM NM_PlegadoresReserva " & _
      " WHERE numero_plegador ='" & pNumPlegador & _
      "' and DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(dFecha) & "') = 0 and centro_costo = '" & _
      pCentroCosto & "' "
      dt = objConn.Query(strsql)
      Return (dt.Rows.Count > 0)
    End Function
    Public Function Actualizar() As Boolean
      'Dim strsql As String, objConn As New NM_Consulta
      'strsql = "UPDATE NM_PlegadoresReserva SET Ubicacion ='" _
      ' & Ubicacion & "',Usuario_creacion='" & Usuario & "'" & _
      ' ",fecha_modificacion= getdate() " & _
      ' " where numero_plegador = '" & NumPlegador & "' " & _
      ' " and DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(Fecha) & "') = 0 and centro_costo ='" & _
      'CentroCosto & "' "
      'Return objConn.Execute(strsql)


      Dim ldtb_Datos As New DataTable, ldtr_row As DataRow
      Dim lobj_xml As New NM_General.Util, lstr_mensaje As String

      'p1 as [Fecha],
      'p2 as [CentroCosto],
      'p3 as [codigo_partida],
      'p4 as [NumPlegador],
      'p5 as [Ubicacion],
      'p6 as [Usuario]

      ldtb_Datos.Columns.Add("p1", GetType(String))
      ldtb_Datos.Columns.Add("p2", GetType(String))
      ldtb_Datos.Columns.Add("p3", GetType(String))
      ldtb_Datos.Columns.Add("p4", GetType(String))
      ldtb_Datos.Columns.Add("p5", GetType(String))
      ldtb_Datos.Columns.Add("p6", GetType(String))
      ldtb_Datos.TableName = "lista"


      ldtr_row = ldtb_Datos.NewRow
      ldtr_row("p1") = Fecha.Year.ToString & Right("0" & Fecha.Month.ToString, 2) & Right("0" & Fecha.Day.ToString, 2) 'YYYYMMDD
      ldtr_row("p2") = CentroCosto
      ldtr_row("p3") = codigo_partida
      ldtr_row("p4") = NumPlegador
      ldtr_row("p5") = Ubicacion
      ldtr_row("p6") = Usuario
      ldtb_Datos.Rows.Add(ldtr_row)

      lstr_mensaje = ""

      Dim lobj_parametros() As Object = {"pchr_accion", "ACT", _
                   "pnvc_datos", lobj_xml.GeneraXml(ldtb_Datos), _
                   "pvch_mensaje", lstr_mensaje}

      lobj_Conexion.EjecutarComando("usp_tej_plegadoresreserva_guardar", lobj_parametros)


    End Function
    Public Function Listar() As DataTable
      Dim objConn As New NM_Consulta
      Return objConn.getData("NM_PlegadoresReserva")
    End Function

    Public Function getPlegadores(ByVal pFecha As Date, ByVal pCentroCosto As String) As DataTable
      Dim strsql As String
      Dim objConn As New NM_Consulta
      strsql = "select * from NM_plegador where codigo_plegador " & _
      " not in (select numero_plegador from NM_PlegadoresReserva " & _
      " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 and centro_costo ='" & _
      pCentroCosto & "' "
      Return objConn.Query(strsql)
    End Function

    Public Function Listar(ByVal pFecha As Date, ByVal pCentroCosto As String) As DataTable
      Dim strsql As String, dt As New DataTable
      Dim objConn As New NM_Consulta
      'Quitar el comentario cuando se trabaje con partida
      'strsql = "Select fecha, centro_costo, codigo_partida, rtrim(numero_plegador) as numero_plegador, ubicacion " & _
      strsql = "Select fecha, centro_costo, rtrim(numero_plegador) as numero_plegador, ubicacion, codigo_partida " & _
      " from NM_PlegadoresReserva " & _
      " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 and centro_costo = '" & _
      pCentroCosto & "' "
      dt = objConn.Query(strsql)
      Return dt
    End Function

  End Class
End Namespace