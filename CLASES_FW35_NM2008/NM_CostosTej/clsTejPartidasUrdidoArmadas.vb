Imports System.Data
Imports System.Data.OleDb
Imports NM.AccesoDatos

Public Class clsTejPartidasUrdidoArmadas

#Region "-- Variables --"

    Protected Friend mConexion As AccesoDatosSQLServer
    Protected Friend mstrError As String

    Dim mstrEmpresa As String = ""
    Dim mnumPeriodo As Double = 0
    Dim mstrUsuario As String = ""

#End Region

    '================================= Definición de constructores ===============================

    '================================= Definición de Propiedades =================================

#Region "-- Propiedades --"

    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property



    Public Property Periodo() As Double
        Get
            Periodo = mnumPeriodo
        End Get
        Set(ByVal numVal As Double)
            mnumPeriodo = numVal
        End Set
    End Property


    Public Property Empresa() As String
        Get
            Empresa = mstrEmpresa
        End Get
        Set(ByVal sCad As String)
            mstrEmpresa = sCad
        End Set
    End Property


    Public Property Usuario() As String
        Get
            Usuario = mstrUsuario
        End Get
        Set(ByVal strCad As String)
            mstrUsuario = strCad
        End Set
    End Property

#End Region

    '=================================== Definicion de metodos  ==================================

    '======== Metodos de Consulta ========


    Public Function ListarDatos(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  cponce
        'Fecha     :      04-08-2011
        'Proposito :      retorna la lista de datos x periodo
        '*******************************************************************************************
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pint_Proceso", clsTejEstadoProceso.Proceso.HilTel_DatosProdUrdido}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            pDT = Conexion.ObtenerDataSet("usp_costej_PartidasUrdidoPorArmadas_Listar", objParametro)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function ImportarDatos() As Boolean
        '*******************************************************************************************
        'Creado por:	  cponce
        'Fecha     :      2011.09.02
        'Proposito :      importa datos de las Partida de Urdido de Produccion
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pvch_Usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_PartidasUrdidoProduccion_Importar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function LimpiarDatos() As Boolean
        '*******************************************************************************************
        'Creado por:	  cponce
        'Fecha     :      2011.09.02
        'Proposito :      limpia los datos de las Partida de Urdido de Produccion
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_PartidasUrdidoPorArmadas_Iniciliza", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function


    Public Function CargarExcel(ByVal mstrArchivo As String) As Boolean
        Dim lobjCon As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" & mstrArchivo & "';Extended Properties='Excel 12.0 Xml;HDR=Yes;'")
        Dim lobjCom As New OleDbCommand("Select * From [rpt_PartidasUrdidoArmadas$]", lobjCon)
        Dim blnRpta As Boolean
        Try
            lobjCon.Open()
            Dim xlReader As OleDbDataReader = lobjCom.ExecuteReader
            Dim Conexion As AccesoDatosSQLServer
            blnRpta = True
            While xlReader.Read
                Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                                "pnum_Periodo", mnumPeriodo, _
                                                "pvch_CodigoPartidaUrdido", xlReader.Item(0), _
                                                "pvch_CodigoCarrete", xlReader.Item(1), _
                                                "pint_HilosCarrete", xlReader.Item(2), _
                                                "pvch_CodigoOperario", xlReader.Item(3), _
                                                "pvch_CodigoSupervisor", xlReader.Item(4), _
                                                "pnum_Roturas", xlReader.Item(5), _
                                                "pnum_RoturasEstandar", xlReader.Item(6), _
                                                "pnum_RoturaMillon", xlReader.Item(7), _
                                                "pvch_CodigoHilo", xlReader.Item(8), _
                                                "pvch_CodigoUrdimbre", xlReader.Item(9), _
                                                "pint_RevisionUrdimbre", xlReader.Item(10), _
                                                "pvch_UsuarioCreacion", mstrUsuario}
                If Not IsDBNull(xlReader.Item(0)) Then
                    Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
                    Conexion.EjecutarComando("usp_costej_PartidasUrdidoPorArmadas_Cargar", objParametro)
                End If
            End While
            xlReader.Close()

        Catch ex As Exception
            blnRpta = False
            Throw ex
        Finally
            lobjCon.Close()
            lobjCon.Dispose()
            lobjCon = Nothing
            lobjCom = Nothing
        End Try
        Return blnRpta = True
    End Function

    ' Verifica Hilos en TMP
    Public Function VerificaHilos(ByRef dstHilosPUP As DataSet, ByVal int_Tipo As Integer) As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pint_Tipo", int_Tipo}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            dstHilosPUP = Conexion.ObtenerDataSet("usp_costej_PartidasUrdidoPorArmadas_Validar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    ' Exportamos la informacion editada de la tabla de produccion temporal
    ' a la original despues de validar los hilos
    Public Function ExportarDatos() As Boolean

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_CodigoEmpresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pvch_Usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_PartidasUrdidoPorArmadas_Importar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function
End Class



