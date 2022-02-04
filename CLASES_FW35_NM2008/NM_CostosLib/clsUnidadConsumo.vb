Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsUnidadConsumo

    '================================ Definicion variables locales ================================
#Region "-- Variables --"

    Protected Friend mstrError As String = ""

    Protected Friend mstrEmpresa As String = ""
    Protected Friend mstrCodigo As String = ""
    Protected Friend mintRevision As Integer = 0
    Protected Friend mstrDescripcion As String = ""
    Protected Friend mstrDescConsumo As String = ""
    Protected Friend mstrCentroCosto As String = ""
    Protected Friend mintTipoUnidad As Int16 = 0
    Protected Friend mintNroMaquinas As Double = 0
    Protected Friend mstrEstado As String = ""
    Protected Friend mstrUsuario As String = ""
    Protected Friend mblnConsumeAgua As Boolean = False
    Protected Friend mblnConsumeEE As Boolean = False
    Protected Friend mblnConsumeGas As Boolean = False
    Protected Friend mblnConsumeVapor As Boolean = False
    Protected Friend mblnConsumeGLP As Boolean = False
    Protected Friend mblnConsumeAire As Boolean = False
    Protected Friend mstrActivoFijo As String = ""
    Protected Friend mstrMaquinaProduccion As String = ""
#End Region

    '================================= Definición de constructores ===============================


    '================================= Definición de Propiedades =================================
#Region "-- Propiedades --"

    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property

    Public Property Empresa() As String
        Get
            Empresa = mstrEmpresa
        End Get
        Set(ByVal sCad As String)
            mstrEmpresa = sCad
        End Set
    End Property

    Public Property Codigo() As String
        Get
            Codigo = mstrCodigo
        End Get
        Set(ByVal strCad As String)
            mstrCodigo = strCad
        End Set
    End Property

    Public Property CentroCosto() As String
        Get
            CentroCosto = mstrCentroCosto
        End Get
        Set(ByVal strCad As String)
            mstrCentroCosto = strCad
        End Set
    End Property

    Public Property Descripcion() As String
        Get
            Descripcion = mstrDescripcion
        End Get
        Set(ByVal strCad As String)
            mstrDescripcion = strCad
        End Set
    End Property

    Public Property DescConsumo() As String
        Get
            DescConsumo = mstrDescConsumo
        End Get
        Set(ByVal strCad As String)
            mstrDescConsumo = strCad
        End Set
    End Property

    Public Property TipoUnidad() As Int16
        Get
            TipoUnidad = mintTipoUnidad
        End Get
        Set(ByVal intVal As Int16)
            mintTipoUnidad = intVal
        End Set
    End Property

    Public Property NroMaquinas() As Integer
        Get
            NroMaquinas = mintNroMaquinas
        End Get
        Set(ByVal intNum As Integer)
            mintNroMaquinas = intNum
        End Set
    End Property

    Public Property Revision() As Integer
        Get
            Revision = mintRevision
        End Get
        Set(ByVal intNum As Integer)
            mintRevision = intNum
        End Set
    End Property

    Public Property Estado() As String
        Get
            Estado = mstrEstado
        End Get
        Set(ByVal strCad As String)
            mstrEstado = strCad
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

    Public Property ConsumeAgua() As Boolean
        Get
            ConsumeAgua = mblnConsumeAgua
        End Get
        Set(ByVal bVal As Boolean)
            mblnConsumeAgua = bVal
        End Set
    End Property

    Public Property ConsumeEE() As Boolean
        Get
            ConsumeEE = mblnConsumeEE
        End Get
        Set(ByVal bVal As Boolean)
            mblnConsumeEE = bVal
        End Set
    End Property

    Public Property ConsumeVapor() As Boolean
        Get
            ConsumeVapor = mblnConsumeVapor
        End Get
        Set(ByVal bVal As Boolean)
            mblnConsumeVapor = bVal
        End Set
    End Property

    Public Property ConsumeGas() As Boolean
        Get
            ConsumeGas = mblnConsumeGas
        End Get
        Set(ByVal bVal As Boolean)
            mblnConsumeGas = bVal
        End Set
    End Property

    Public Property ConsumeGLP() As Boolean
        Get
            ConsumeGLP = mblnConsumeGLP
        End Get
        Set(ByVal bVal As Boolean)
            mblnConsumeGLP = bVal
        End Set
    End Property

    Public Property ConsumeAire() As Boolean
        Get
            ConsumeAire = mblnConsumeAire
        End Get
        Set(ByVal bVal As Boolean)
            mblnConsumeAire = bVal
        End Set
    End Property

    Public Property ActivoFijo() As String
        Get
            ActivoFijo = mstrActivoFijo
        End Get
        Set(ByVal sCad As String)
            mstrActivoFijo = sCad
        End Set
    End Property

    Public Property MaquinaProduccion() As String
        Get
            MaquinaProduccion = mstrMaquinaProduccion
        End Get
        Set(ByVal sCad As String)
            mstrMaquinaProduccion = sCad
        End Set
    End Property

#End Region

    '=================================== Definicion de metodos  ==================================
#Region "-- Metodos --"

    Public Function Listar(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      01-12-2009
        'Proposito :      retorna un listado de las unidades de consumo total
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 2, _
                                        "pvch_CodigoUnidadConsumo", mstrCodigo, _
                                        "pchr_Empresa", mstrEmpresa, _
                                        "pint_RevisionUnidadConsumo", mintRevision, _
                                        "pvch_CodigoCentroCosto", mstrCentroCosto, _
                                        "pvch_descripcion", mstrDescripcion, _
                                        "pvch_estado", mstrEstado}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataTable("usp_cos_unidadconsumo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Sub Obtener(ByRef dtbDataSet As DataSet)
        '************************************************  
        'Creado por:	  Edwin Poma
        'Fecha     :      2009.12.01
        'Proposito :      Obtiene los datos de una unidad de consumo y los listados de consumo de agua, EE, Gas,glp, aire tablas: 0-unidad de consumo, 1-datos de agua, 2-datos de EE, 3-datos de gas
        '************************************************  
        Dim Conexion As AccesoDatosSQLServer
        'Dim dtbDataSet As DataSet
        Dim objParametro() As Object = {"ptin_tipolista", 1, _
                                        "pvch_CodigoUnidadConsumo", mstrCodigo, _
                                        "pchr_Empresa", mstrEmpresa, _
                                        "pint_RevisionUnidadConsumo", mintRevision, _
                                        "pvch_CodigoCentroCosto", mstrCentroCosto, _
                                        "pvch_descripcion", mstrDescripcion, _
                                        "pvch_estado", mstrEstado}
        Try
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            dtbDataSet = Conexion.ObtenerDataSet("usp_cos_unidadconsumo_listar", objParametro)

            'If (Not (dtbData Is Nothing)) AndAlso dtbData.Rows.Count > 0 Then
            '    mstrCodigo = dtbData.Rows(0).Item("vch_CodigoUnidadConsumo").ToString
            '    mstrDescripcion = dtbData.Rows(0).Item("vch_Descripcion").ToString
            '    mstrDescConsumo = dtbData.Rows(0).Item("vch_DescripcionConsumo").ToString
            '    mstrCentroCosto = dtbData.Rows(0).Item("vch_CodigoCentroCosto").ToString
            '    mintTipoUnidad = dtbData.Rows(0).Item("tin_TipoUnidad")
            '    mintNroMaquinas = dtbData.Rows(0).Item("vch_CodigoUnidadConsumo")
            '    mstrEstado = dtbData.Rows(0).Item("vch_Estado").ToString
            '    mstrUsuario = dtbData.Rows(0).Item("vch_UsuarioCreacion").ToString
            '    mblnConsumeAgua = dtbData.Rows(0).Item("bit_ConsumeAgua")
            '    mblnConsumeEE = dtbData.Rows(0).Item("bit_ConsumeEE")
            '    mblnConsumeGas = dtbData.Rows(0).Item("bit_ConsumeGas")
            '    mblnConsumeVapor = dtbData.Rows(0).Item("bit_ConsumeVapor")
            '    mstrActivoFijo = dtbData.Rows(0).Item("vch_ActivoFijo").ToString
            '    mstrMaquinaProduccion = dtbData.Rows(0).Item("vch_MaquinaProduccion").ToString
            'Else
            '    mstrEmpresa = ""
            '    mstrCodigo = ""
            '    mintRevision = 0
            '    mstrDescripcion = ""
            '    mstrDescConsumo = ""
            '    mstrCentroCosto = ""
            '    mintTipoUnidad = 0
            '    mintNroMaquinas = 0
            '    mstrEstado = ""
            '    mstrUsuario = ""
            '    mblnConsumeAgua = False
            '    mblnConsumeEE = False
            '    mblnConsumeGas = False
            '    mblnConsumeVapor = False
            '    mstrActivoFijo = ""
            '    mstrMaquinaProduccion = 0
            'End If

        Catch ex As Exception
            mstrError = ex.Message
        End Try

    End Sub

    '===== Metodos de Actualizacion  =====

    Public Function ObtenerEsquemas(ByRef pdtDetalleAgua As DataTable, ByRef pdtDetalleEE As DataTable, ByRef pdtDetalleGas As DataTable, ByRef pdtDetalleGLP As DataTable, ByRef pdtDetalleVapor As DataTable, ByRef pdtDetalleAire As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.01.05
        'Proposito :      Devuelve los esquemas necesarios donde se va a guardar los datos en xml (Agua, EE, Gas)
        '*******************************************************************************************
        Dim blnRpta As Boolean, pDT As New DataSet, lclsEsquemas As New clsEsquemas

        blnRpta = lclsEsquemas.ObtenerEsquemas(pDT, clsEsquemas.enu_esquemas.GuardarDetalleUConsumo)
        If blnRpta = True Then
            pdtDetalleAgua = pDT.Tables(0)
            pdtDetalleEE = pDT.Tables(1)
            pdtDetalleGas = pDT.Tables(2)
            pdtDetalleGLP = pDT.Tables(3)
            pdtDetalleVapor = pDT.Tables(4)
            pdtDetalleAire = pDT.Tables(5)
        End If

        'Agua
        'bit_DistServicioLimpieza	as [c1]

        'EE
        'tin_GrupoCalculo					as [c1],
        'vch_CodigoPlanta					as [c2],	
        'vch_CodigoTipoTelar				as [c3],
        'bit_HorasEstandar					as [c4],
        'bit_EmpleaVelocidad				as [c5],
        'bit_Luminosidad					as [c6],
        'bit_Luwa							as [c7],
        'chr_LineaProduccion				as [c8],
        'bit_TrabajaHilo					as [c9],
        'int_FormulaPPRPotenciaKw			as [c10],
        'int_FormulaPPRConsumoFijoKw		as [c11],
        'int_FormulaPPRConsumoVariableKw	as [c12],
        'chr_CodigoPlanta					as [c13]

        'Gas
        'tin_GrupoCalculo	as [c1]

        Try
            mstrError = ""
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally

        End Try

        Return blnRpta

    End Function

    Public Function Insertar(ByVal pdtDetalleAgua As DataTable, ByVal pdtDetalleEE As DataTable, ByVal pdtDetalleGas As DataTable, ByVal pdtDetalleGLP As DataTable, ByVal pdtDetalleVapor As DataTable, ByVal pdtDetalleAire As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      19-11-2009
        'Proposito :      Permite registrar una nueva unidad de consumo
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer

        pdtDetalleAgua.TableName = "listaagua"
        pdtDetalleEE.TableName = "listaee"
        pdtDetalleGas.TableName = "listagas"
        'pdtDetalleGLP.TableName = "listaglp"
        pdtDetalleVapor.TableName = "listavapor"
        pdtDetalleAire.TableName = "listaaire"

        Dim objParametro() As Object = {"ptin_accion", 1, _
                                        "pchr_CodigoEmpresaUndCon", mstrEmpresa, _
                                        "pvch_CodigoUnidadConsumo", mstrCodigo, _
                                        "pint_RevisionUnidadConsumo", mintRevision, _
                                        "pvch_CodigoCentroCosto", mstrCentroCosto, _
                                        "pchr_CodigoEmpresaCentroCosto", mstrEmpresa, _
                                        "pvch_Descripcion", mstrDescripcion, _
                                        "pvch_DescripcionConsumo", mstrDescConsumo, _
                                        "pbit_ConsumeAgua", IIf(mblnConsumeAgua = True, 1, 0), _
                                        "pbit_ConsumeEE", IIf(mblnConsumeEE = True, 1, 0), _
                                        "pbit_ConsumeVapor", IIf(mblnConsumeVapor = True, 1, 0), _
                                        "pbit_ConsumeGas", IIf(mblnConsumeGas = True, 1, 0), _
                                        "pbit_ConsumeGLP", IIf(mblnConsumeGLP = True, 1, 0), _
                                        "pbit_ConsumeAire", IIf(mblnConsumeAire = True, 1, 0), _
                                        "pvch_ActivoFijo", mstrActivoFijo, _
                                        "pint_NumeroMaquinas", mintNroMaquinas, _
                                        "pvch_Estado", "ACT", _
                                        "ptin_TipoUnidad", mintTipoUnidad, _
                                        "pvch_MaquinaProduccion", mstrMaquinaProduccion, _
                                        "pvch_Usuario", mstrUsuario, _
                                        "pnte_detalleagua", clsUtilitario.GeneraXml(pdtDetalleAgua), _
                                        "pnte_detalleee", clsUtilitario.GeneraXml(pdtDetalleEE), _
                                        "pnte_detallegas", clsUtilitario.GeneraXml(pdtDetalleGas), _
                                        "pnte_detalleglp", "", _
                                        "pnte_detallevapor", clsUtilitario.GeneraXml(pdtDetalleVapor), _
                                         "pnte_detalleaire", clsUtilitario.GeneraXml(pdtDetalleAire)}


        clsUtilitario = Nothing
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_unidadconsumo_guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function Modificar(ByVal pdtDetalleAgua As DataTable, ByVal pdtDetalleEE As DataTable, ByVal pdtDetalleGas As DataTable, ByVal pdtDetalleGLP As DataTable, ByVal pdtDetalleVapor As DataTable, ByVal pdtDetalleAire As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      Permite modificar datos de la unidad de consumo
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer

        pdtDetalleAgua.TableName = "listaagua"
        pdtDetalleEE.TableName = "listaee"
        pdtDetalleGas.TableName = "listagas"
        'pdtDetalleGLP.TableName = "listaglp"
        pdtDetalleVapor.TableName = "listavapor"
        pdtDetalleAire.TableName = "listaaire"

        Dim objParametro() As Object = {"ptin_accion", 2, _
                                        "pchr_CodigoEmpresaUndCon", mstrEmpresa, _
                                        "pvch_CodigoUnidadConsumo", mstrCodigo, _
                                        "pvch_CodigoCentroCosto", mstrCentroCosto, _
                                        "pint_RevisionUnidadConsumo", mintRevision, _
                                        "pchr_CodigoEmpresaCentroCosto", mstrEmpresa, _
                                        "pvch_Descripcion", mstrDescripcion, _
                                        "pvch_DescripcionConsumo", mstrDescConsumo, _
                                        "pbit_ConsumeAgua", IIf(mblnConsumeAgua = True, 1, 0), _
                                        "pbit_ConsumeEE", IIf(mblnConsumeEE = True, 1, 0), _
                                        "pbit_ConsumeVapor", IIf(mblnConsumeVapor = True, 1, 0), _
                                        "pbit_ConsumeGas", IIf(mblnConsumeGas = True, 1, 0), _
                                        "pbit_ConsumeGLP", IIf(mblnConsumeGLP = True, 1, 0), _
                                        "pbit_ConsumeAire", IIf(mblnConsumeAire = True, 1, 0), _
                                        "pvch_ActivoFijo", mstrActivoFijo, _
                                        "pint_NumeroMaquinas", mintNroMaquinas, _
                                        "pvch_Estado", "ACT", _
                                        "ptin_TipoUnidad", mintTipoUnidad, _
                                        "pvch_MaquinaProduccion", mstrMaquinaProduccion, _
                                        "pvch_Usuario", mstrUsuario, _
                                        "pnte_detalleagua", clsUtilitario.GeneraXml(pdtDetalleAgua), _
                                        "pnte_detalleee", clsUtilitario.GeneraXml(pdtDetalleEE), _
                                        "pnte_detallegas", clsUtilitario.GeneraXml(pdtDetalleGas), _
                                        "pnte_detalleglp", "", _
                                        "pnte_detallevapor", clsUtilitario.GeneraXml(pdtDetalleVapor), _
                                         "pnte_detalleaire", clsUtilitario.GeneraXml(pdtDetalleAire)}

        clsUtilitario = Nothing
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_unidadconsumo_guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function Eliminar() As Boolean
        '*******************************************************************************************
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      Elimina logicamente la unidad de consumo
        '*******************************************************************************************
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"ptin_accion", 3, _
                                        "pchr_CodigoEmpresaUndCon", mstrEmpresa, _
                                        "pvch_CodigoUnidadConsumo", mstrCodigo, _
                                        "pint_RevisionUnidadConsumo", mintRevision, _
                                        "pvch_CodigoCentroCosto", mstrCentroCosto, _
                                        "pchr_CodigoEmpresaCentroCosto", mstrEmpresa, _
                                        "pvch_Descripcion", mstrDescripcion, _
                                        "pvch_DescripcionConsumo", mstrDescConsumo, _
                                        "pbit_ConsumeAgua", IIf(mblnConsumeAgua = True, 1, 0), _
                                        "pbit_ConsumeEE", IIf(mblnConsumeEE = True, 1, 0), _
                                        "pbit_ConsumeVapor", IIf(mblnConsumeVapor = True, 1, 0), _
                                        "pbit_ConsumeGas", IIf(mblnConsumeGas = True, 1, 0), _
                                        "pbit_ConsumeGLP", IIf(mblnConsumeGLP = True, 1, 0), _
                                        "pbit_ConsumeAire", IIf(mblnConsumeAire = True, 1, 0), _
                                        "pvch_ActivoFijo", mstrActivoFijo, _
                                        "pint_NumeroMaquinas", mintNroMaquinas, _
                                        "pvch_Estado", "ANU", _
                                        "ptin_TipoUnidad", mintTipoUnidad, _
                                        "pvch_MaquinaProduccion", mstrMaquinaProduccion, _
                                        "pvch_Usuario", mstrUsuario, _
                                        "pnte_detalleagua", "", _
                                        "pnte_detalleee", "", _
                                        "pnte_detallegas", "", _
                                        "pnte_detalleglp", "", _
                                        "pnte_detallevapor", "", _
                                         "pnte_detalleaire", ""}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_unidadconsumo_guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function Activar() As Boolean
        '*******************************************************************************************
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      Elimina logicamente la unidad de consumo
        '*******************************************************************************************
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"ptin_accion", 4, _
                                        "pchr_CodigoEmpresaUndCon", mstrEmpresa, _
                                        "pvch_CodigoUnidadConsumo", mstrCodigo, _
                                        "pint_RevisionUnidadConsumo", mintRevision, _
                                        "pvch_CodigoCentroCosto", mstrCentroCosto, _
                                        "pchr_CodigoEmpresaCentroCosto", mstrEmpresa, _
                                        "pvch_Descripcion", mstrDescripcion, _
                                        "pvch_DescripcionConsumo", mstrDescConsumo, _
                                        "pbit_ConsumeAgua", IIf(mblnConsumeAgua = True, 1, 0), _
                                        "pbit_ConsumeEE", IIf(mblnConsumeEE = True, 1, 0), _
                                        "pbit_ConsumeVapor", IIf(mblnConsumeVapor = True, 1, 0), _
                                        "pbit_ConsumeGas", IIf(mblnConsumeGas = True, 1, 0), _
                                        "pbit_ConsumeGLP", IIf(mblnConsumeGLP = True, 1, 0), _
                                        "pbit_ConsumeAire", IIf(mblnConsumeAire = True, 1, 0), _
                                        "pvch_ActivoFijo", mstrActivoFijo, _
                                        "pint_NumeroMaquinas", mintNroMaquinas, _
                                        "pvch_Estado", "ANU", _
                                        "ptin_TipoUnidad", mintTipoUnidad, _
                                        "pvch_MaquinaProduccion", mstrMaquinaProduccion, _
                                        "pvch_Usuario", mstrUsuario, _
                                        "pnte_detalleagua", "", _
                                        "pnte_detalleee", "", _
                                        "pnte_detallegas", "", _
                                        "pnte_detalleglp", "", _
                                        "pnte_detallevapor", "", _
                                         "pnte_detalleaire", ""}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_unidadconsumo_guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

#End Region

End Class
