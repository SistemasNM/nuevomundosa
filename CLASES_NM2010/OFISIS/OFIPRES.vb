Imports NuevoMundo.Generales
Namespace OFIPRES
    Public Class Areas
        Inherits Clases.General
        Implements Interfases.IOFISIS

#Region "    Constantes"
        Private CONST_SP_LISTAR = "usp_qry_AreasListar"
        Private Const CONST_NOMBRE_TABLA_AREAS = "AREAS"
#End Region

#Region "    Constructor"
        Sub New(ByVal pstrEmpresa As String, ByVal pstrUsuario As String)
            Me.EmpresaCodigo = pstrEmpresa
            Me.UsuarioBD = pstrUsuario
        End Sub
#End Region

#Region "    Metodos"

    Public Function Listar(ByRef pLista As DataTable, ByVal ParamArray Flags() As String) As Boolean Implements Interfases.IOFISIS.Listar
      Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
      Dim lbooOk As Boolean
      Dim lstrParams() As String = {"CO_AREA", Flags(0), "NO_AREA", Flags(1)}

      Try
        lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.PresupuestoOfisis)
        pLista = lobjCon.ObtenerDataTable(CONST_SP_LISTAR, lstrParams)
        pLista.TableName = CONST_NOMBRE_TABLA_AREAS
        lbooOk = True
      Catch ex As Exception
        pLista = New DataTable
        lbooOk = False
      Finally
        lobjCon = Nothing
      End Try
      Return lbooOk
    End Function


    Public Function Buscar() As Boolean Implements Interfases.IOFISIS.Buscar
    End Function
    Public Sub Dispose() Implements Interfases.IOFISIS.Dispose
    End Sub
#End Region

  End Class

  Public Class Presupuesto
    Public Function ListaGastoMensual(ByVal sCO_EMPR As String, _
                                  ByVal sNU_REGI_EMPR As String, _
                                  ByVal sNU_ANNO As String, _
                                  ByVal sNU_MESE As String, _
                                  ByVal sTI_AUXI_EMPR As String, _
                                  ByVal sCO_AUXI_EMPR As String, _
                                  ByVal sCO_MONE As String, _
                                  ByVal sOP_REPO As String, _
                                  ByVal sCO_USUA As String) As DataTable
      Dim lobjConexion As NM.AccesoDatos.AccesoDatosSQLServer, ldtbRetornar As DataTable
      Try
        Dim lstrParametros() As String = {"ISCO_EMPR", sCO_EMPR, _
                                          "INNU_REGI_EMPR", sNU_REGI_EMPR, _
                                          "INNU_ANNO", sNU_ANNO, _
                                          "INNU_MESE", sNU_MESE, _
                                          "ISTI_AUXI_EMPR", sTI_AUXI_EMPR, _
                                          "ISCO_AUXI_EMPR", sCO_AUXI_EMPR, _
                                          "ISCO_MONE", sCO_MONE, _
                                          "ISOP_REPO", sOP_REPO, _
                                          "ISCO_USUA", sCO_USUA}

        lobjConexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.PresupuestoOfisis)
        ldtbRetornar = lobjConexion.ObtenerDataTable("NM_GASTOS_MENSUALES", lstrParametros)

      Catch ex As Exception

      Finally
        lobjConexion = Nothing
      End Try
      Return ldtbRetornar
    End Function

  End Class
End Namespace
