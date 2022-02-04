Namespace OFISIS.OFIPLAN
  Public Class Trabajador
#Region "   Variables"
    Private mstrCodigo As String
    Private mstrApellidoPaterno As String
    Private mstrApellidoMaterno As String
    Private mstrNombres As String
    Private mbooActivo As Boolean
#End Region
#Region "   Constructor"
    Sub New()
      mstrCodigo = ""
      mstrApellidoMaterno = ""
      mstrApellidoPaterno = ""
      mstrNombres = ""
    End Sub
    Sub New(ByVal strCodigo As String)
      mstrCodigo = strCodigo
      mstrApellidoMaterno = ""
      mstrApellidoPaterno = ""
      mstrNombres = ""
    End Sub
#End Region
#Region "   Propiedades"

    Public Property Codigo() As String
      Get
        Codigo = mstrCodigo
      End Get
      Set(ByVal Value As String)
        mstrCodigo = Value
      End Set
    End Property

    Public Property ApellidoPaterno() As String
      Get
        ApellidoPaterno = mstrApellidoPaterno
      End Get
      Set(ByVal Value As String)
        mstrApellidoPaterno = Value
      End Set
    End Property
    Public Property ApellidoMaterno() As String
      Get
        ApellidoMaterno = mstrApellidoMaterno
      End Get
      Set(ByVal Value As String)
        mstrApellidoMaterno = Value
      End Set
    End Property
    Public Property Nombres() As String
      Get
        Nombres = mstrNombres
      End Get
      Set(ByVal Value As String)
        mstrNombres = Value
      End Set
    End Property
    Public Property Activo() As Boolean
      Get
        Activo = mbooActivo
      End Get
      Set(ByVal Value As Boolean)
        mbooActivo = Value
      End Set
    End Property
#End Region
#Region "   Metodos"
    Public Function Buscar()
      Dim lobjOFIPLAN As NM.AccesoDatos.AccesoDatosSQLServer
      Dim lstrParametros() As String = {"var_CodigoTrabajador", mstrCodigo}
      Dim ldtRes As DataTable
      Try
        lobjOFIPLAN = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
        ldtRes = lobjOFIPLAN.ObtenerDataTable("usp_PLA_Trabajador_Buscar", lstrParametros)
        If ldtRes.Rows.Count = 1 Then
          With ldtRes.Rows(0)
            mstrApellidoPaterno = .Item("apellido_paterno")
            mstrApellidoMaterno = .Item("apellido_materno")
            mstrNombres = .Item("nombres")
            mbooActivo = IIf(.Item("estado") = "ACT", True, False)
          End With
        Else
          mstrApellidoPaterno = ""
          mstrApellidoMaterno = ""
          mstrNombres = ""
          mbooActivo = False
        End If
      Catch ex As Exception

      Finally
        lobjOFIPLAN = Nothing
      End Try
    End Function
    Public Function Listar(ByVal pstrCodigo As String, ByVal pstrNombres As String, ByVal pstrAppPaterno As String, ByVal pstrAppMaterno As String, ByVal pstrEstado As String) As DataTable
      Dim lobjOFIPLAN As NM.AccesoDatos.AccesoDatosSQLServer
      Dim lstrParametros() As String = {"var_CodigoTrabajador", mstrCodigo, _
                          "var_NombresTrabajador", pstrNombres, _
                          "var_AppPaternoTrabajador", pstrAppPaterno, _
                          "var_AppMaternoTrabajador", pstrAppMaterno, _
                          "var_Estado", pstrEstado}
      Dim ldtRes As DataTable
      Try
        lobjOFIPLAN = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
        ldtRes = lobjOFIPLAN.ObtenerDataTable("usp_PLA_Trabajador_Listar", lstrParametros)

      Catch ex As Exception

      Finally
        lobjOFIPLAN = Nothing
      End Try
      Return ldtRes
    End Function
    Public Function NombreCompleto() As String
      Dim lstrNC As String = ""
      If mstrApellidoPaterno <> "" Then
        If lstrNC = "" Then
          lstrNC = mstrApellidoPaterno
        Else
          lstrNC = lstrNC + " " + mstrApellidoPaterno
        End If
      End If
      If mstrApellidoMaterno <> "" Then
        If lstrNC = "" Then
          lstrNC = mstrApellidoMaterno
        Else
          lstrNC = lstrNC + " " + mstrApellidoMaterno
        End If
      End If
      If mstrNombres <> "" Then
        If lstrNC = "" Then
          lstrNC = mstrNombres
        Else
          lstrNC = lstrNC + ", " + mstrNombres
        End If
      End If
      Return lstrNC
    End Function
#End Region
  End Class
End Namespace
