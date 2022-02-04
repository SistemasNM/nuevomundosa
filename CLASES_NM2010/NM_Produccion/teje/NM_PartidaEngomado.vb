Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Tejeduria
    Public Class NM_PartidaEngomado
        Inherits NM_PArtidaEngomadoYTED
        Const CALIDAD_FILETA1 As String = "FILETA1"
        Const CALIDAD_FILETA2 As String = "FILETA2"
        Const CALIDAD_GRUPO1 As String = "GRUPO1"
        Const CALIDAD_GRUPO2 As String = "GRUPO2"
        Const CALIDAD_MARCHA_LENTA As String = "MLENTA"
        Const CALIDAD_TINA1 As String = "TINA1"
        Const CALIDAD_TINA2 As String = "TINA2"
        Const CALIDAD_CABEZAL As String = "CABEZAL"
        Const COD_FASE As String = "3"        ' Fase de Engomado
        Const COD_1DCALIDAD As String = "1"   ' código del primer detalle de calidad
        Private _objConn As AccesoDatosSQLServer

        Public Sub New()
            tipo = COD_FASE
            _objConn = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        End Sub

        Overrides Sub Seek(ByVal pcodPartidaEngomado As String)
            MyBase.Seek(pcodPartidaEngomado)
            Dim engomadoCalidad As New NM_PartidaEngomadoDCalidad
            dtCalidad = engomadoCalidad.Listar(pcodPartidaEngomado)
        End Sub

        Public Overrides Sub AgregarProduccion(ByVal codPartidEngoProduct As String, ByVal plegador As String, ByVal lado As String, _
          ByVal longitud As Integer, ByVal cantPiezas As Integer, ByVal Operario As String, _
          ByVal ocurrencia As String, ByVal pCodSupervisor As String, _
          ByVal pfechaInicio As String, ByVal pfechaFinal As String, ByVal codMaquina As String, ByVal pUsuario As String, ByVal pObsvPlegador As String)
            MyBase.AgregarProduccion(codPartidEngoProduct, plegador, lado, longitud, cantPiezas, Operario, ocurrencia, pCodSupervisor, pfechaInicio, pfechaFinal, codMaquina, pUsuario, pObsvPlegador)
            ' Se inserta una fila vacia en Calidad (en paralelo)
            Seek(codPartidEngoProduct)
            Calidad_Agregar(plegador)
        End Sub

        Public Function GetCodigosEngomado(ByVal pCod_sub_Part_Urd As String) As DataTable
            Try
                Dim objParametros() As Object = {"PARTIDA_URDIDO", pCod_sub_Part_Urd}
                Return _objConn.ObtenerDataTable("SP_NM_PARTIDA_URDIDO_ENGOMADO_OBTENER", objParametros)
            Catch Ex As Exception
                Throw Ex
            End Try
        End Function

        Public Function Rep_Eficiencia_partidas(ByVal dtInicio As String, ByVal dtFinal As String, _
        ByVal strPartida As String, ByVal strTelar As String, ByVal strUrdimbre As String, ByVal strPlanta As String) As DataSet
            Try
                Dim objParametros() As Object = {"INICIO", dtInicio, "FINAL", dtFinal, _
                "CODIGO_PARTIDA", strPartida, "CODIGO_TELAR", strTelar, "CODIGO_URDIMBRE", strUrdimbre, "CODIGO_PLANTA", strPlanta}
                Return _objConn.ObtenerDataSet("SP_NM_REP_EFIC_PARTIDA1", objParametros)
            Catch Ex As Exception
                Throw Ex
            End Try
        End Function

        ' Agrega una fila vacía en la vista de calidad
        Public Sub Calidad_Agregar(ByVal codigoPlegador As String)
            Dim partidaEngomadoDCalidad As New NM_PartidaEngomadoDCalidad
            Dim partidaEngomadoMCalidad As New NM_PartidaEngomadoMCalidad
            partidaEngomadoDCalidad.Insertar(codPartidaEngomado, codigoPlegador, CALIDAD_FILETA1, COD_1DCALIDAD, 0)
            partidaEngomadoDCalidad.Insertar(codPartidaEngomado, codigoPlegador, CALIDAD_FILETA2, COD_1DCALIDAD, 0)
            partidaEngomadoDCalidad.Insertar(codPartidaEngomado, codigoPlegador, CALIDAD_GRUPO1, COD_1DCALIDAD, 0)
            partidaEngomadoDCalidad.Insertar(codPartidaEngomado, codigoPlegador, CALIDAD_GRUPO2, COD_1DCALIDAD, 0)
            partidaEngomadoDCalidad.Insertar(codPartidaEngomado, codigoPlegador, CALIDAD_MARCHA_LENTA, COD_1DCALIDAD, 0)
            partidaEngomadoMCalidad.Codigo_partida_engomado = codPartidaEngomado
            partidaEngomadoMCalidad.Codigo_plegador = codigoPlegador
            partidaEngomadoMCalidad.Otros = 0
            partidaEngomadoMCalidad.calificacion = 0
            partidaEngomadoMCalidad.Insertar()
            ' ---- Agregar : Tina1, Tina2 , Cabezal ----
            MaestroDCalidad_Inicializar(codPartidaEngomado, codigoPlegador, CALIDAD_TINA1)
            MaestroDCalidad_Inicializar(codPartidaEngomado, codigoPlegador, CALIDAD_TINA2)
            MaestroDCalidad_Inicializar(codPartidaEngomado, codigoPlegador, CALIDAD_CABEZAL)
        End Sub

        Public Sub Calidad_Actualizar(ByVal codigoPlegador As String, ByVal fileta1 As Integer, _
         ByVal fileta2 As Integer, ByVal grupo1 As Integer, ByVal grupo2 As Integer, _
         ByVal marchaLenta As Integer, ByVal otros As Integer)

            Dim partidaEngomadoMCalidad As New NM_PartidaEngomadoMCalidad
            Dim partidaEngomadoDCalidad As New NM_PartidaEngomadoDCalidad
            partidaEngomadoDCalidad.Actualizar(codPartidaEngomado, codigoPlegador, CALIDAD_FILETA1, COD_1DCALIDAD, fileta1)
            partidaEngomadoDCalidad.Actualizar(codPartidaEngomado, codigoPlegador, CALIDAD_FILETA2, COD_1DCALIDAD, fileta2)
            partidaEngomadoDCalidad.Actualizar(codPartidaEngomado, codigoPlegador, CALIDAD_GRUPO1, COD_1DCALIDAD, grupo1)
            partidaEngomadoDCalidad.Actualizar(codPartidaEngomado, codigoPlegador, CALIDAD_GRUPO2, COD_1DCALIDAD, grupo2)
            partidaEngomadoDCalidad.Actualizar(codPartidaEngomado, codigoPlegador, CALIDAD_MARCHA_LENTA, COD_1DCALIDAD, marchaLenta)
            partidaEngomadoMCalidad.Codigo_partida_engomado = codPartidaEngomado
            partidaEngomadoMCalidad.Codigo_plegador = codigoPlegador
            partidaEngomadoMCalidad.Otros = otros
            partidaEngomadoMCalidad.Actualizar()

            ' Calcular el valor de la calificacion
            CalcularCalificacion(codigoPlegador)
        End Sub

        Public Sub Calidad_Eliminar(ByVal codigoPlegador As String)
            Dim partidaEngomadoMCalidad As New NM_PartidaEngomadoMCalidad
            Dim partidaEngomadoDCalidad As New NM_PartidaEngomadoDCalidad
            partidaEngomadoMCalidad.Eliminar(codPartidaEngomado, codigoPlegador)
            partidaEngomadoDCalidad.Eliminar(codPartidaEngomado, codigoPlegador, CALIDAD_FILETA1, COD_1DCALIDAD)
            partidaEngomadoDCalidad.Eliminar(codPartidaEngomado, codigoPlegador, CALIDAD_FILETA2, COD_1DCALIDAD)
            partidaEngomadoDCalidad.Eliminar(codPartidaEngomado, codigoPlegador, CALIDAD_GRUPO1, COD_1DCALIDAD)
            partidaEngomadoDCalidad.Eliminar(codPartidaEngomado, codigoPlegador, CALIDAD_GRUPO2, COD_1DCALIDAD)
            partidaEngomadoDCalidad.Eliminar(codPartidaEngomado, codigoPlegador, CALIDAD_MARCHA_LENTA, COD_1DCALIDAD)
            ' ---- Eliminar : Tina1, Tina2 , Cabezal ----
            MaestroDCalidad_Eliminar(codPartidaEngomado, codigoPlegador, CALIDAD_TINA1)
            MaestroDCalidad_Eliminar(codPartidaEngomado, codigoPlegador, CALIDAD_TINA2)
            MaestroDCalidad_Eliminar(codPartidaEngomado, codigoPlegador, CALIDAD_CABEZAL)
        End Sub

        ' Crea todos los detalles de calidad, que pertenecen al criterio de calidad <codigoMaestroCalidad>, con valor cero
        Private Sub MaestroDCalidad_Inicializar(ByVal codigoPartidaEngomado As String, ByVal codigoPlegador As String, ByVal codigoMaestroCalidad As String)
            Dim partidaEngomadoDCalidad As New NM_PartidaEngomadoDCalidad
            Dim dtDetalleCalidad As DataTable
            Dim dr As DataRow
            dtDetalleCalidad = partidaEngomadoDCalidad.Listar(codigoPartidaEngomado, codigoPlegador, codigoMaestroCalidad)
            Dim i As Integer
            For i = 0 To dtDetalleCalidad.Rows.Count - 1
                partidaEngomadoDCalidad.Insertar(codigoPartidaEngomado, codigoPlegador, codigoMaestroCalidad, dtDetalleCalidad.Rows(i).Item("codigo_detalle_calidad"), 0)
                ' REVISAR: el data table se vuelve vacío, por eso se vuelve a cargar nuevamente
                dtDetalleCalidad = partidaEngomadoDCalidad.Listar(codigoPartidaEngomado, codigoPlegador, codigoMaestroCalidad)
            Next
        End Sub

        Private Sub MaestroDCalidad_Eliminar(ByVal codigoPartidaEngomado As String, ByVal codigoPlegador As String, ByVal codigoMaestroCalidad As String)
            Dim partidaEngomadoDCalidad As New NM_PartidaEngomadoDCalidad
            partidaEngomadoDCalidad.Eliminar(codigoPartidaEngomado, codigoPlegador, codigoMaestroCalidad)
        End Sub

        Public Sub InsumosQuimicos_Agregar(ByVal codigoInsumo As String, _
          ByVal preparacion1 As String, ByVal preparacion2 As String, _
          ByVal preparacion3 As String, ByVal preparacion4 As String, _
          ByVal preparacion5 As String, ByVal preparacion6 As String)

            Dim partidaEngomadoDInsumosQuimicos As New NM_PartidaEngomadoDInsumosQuimicos
            ' Verificar si el campo no es vacío, en caso contario insertar valor cero
            If preparacion1 <> "" Then
                partidaEngomadoDInsumosQuimicos.Insertar(codPartidaEngomado, 1, codigoInsumo, preparacion1)
            Else
                partidaEngomadoDInsumosQuimicos.Insertar(codPartidaEngomado, 1, codigoInsumo, 0)
            End If
            If preparacion2 <> "" Then
                partidaEngomadoDInsumosQuimicos.Insertar(codPartidaEngomado, 2, codigoInsumo, preparacion2)
            Else
                partidaEngomadoDInsumosQuimicos.Insertar(codPartidaEngomado, 2, codigoInsumo, 0)
            End If
            If preparacion3 <> "" Then
                partidaEngomadoDInsumosQuimicos.Insertar(codPartidaEngomado, 3, codigoInsumo, preparacion3)
            Else
                partidaEngomadoDInsumosQuimicos.Insertar(codPartidaEngomado, 3, codigoInsumo, 0)
            End If
            If preparacion4 <> "" Then
                partidaEngomadoDInsumosQuimicos.Insertar(codPartidaEngomado, 4, codigoInsumo, preparacion4)
            Else
                partidaEngomadoDInsumosQuimicos.Insertar(codPartidaEngomado, 4, codigoInsumo, 0)
            End If
            If preparacion5 <> "" Then
                partidaEngomadoDInsumosQuimicos.Insertar(codPartidaEngomado, 5, codigoInsumo, preparacion5)
            Else
                partidaEngomadoDInsumosQuimicos.Insertar(codPartidaEngomado, 5, codigoInsumo, 0)
            End If
            If preparacion6 <> "" Then
                partidaEngomadoDInsumosQuimicos.Insertar(codPartidaEngomado, 6, codigoInsumo, preparacion6)
            Else
                partidaEngomadoDInsumosQuimicos.Insertar(codPartidaEngomado, 6, codigoInsumo, 0)
            End If
        End Sub

        Public Sub InsumosQuimicos_Actualizar(ByVal codigoInsumo As String, _
         ByVal preparacion1 As String, ByVal preparacion2 As String, _
         ByVal preparacion3 As String, ByVal preparacion4 As String, _
         ByVal preparacion5 As String, ByVal preparacion6 As String)
            Dim partidaEngomadoDInsumosQuimicos As New NM_PartidaEngomadoDInsumosQuimicos
            If preparacion1 = "" Then
                partidaEngomadoDInsumosQuimicos.Actualizar(codPartidaEngomado, codigoInsumo, 1, 0)
            Else
                partidaEngomadoDInsumosQuimicos.Actualizar(codPartidaEngomado, codigoInsumo, 1, preparacion1)
            End If
            If preparacion2 = "" Then
                partidaEngomadoDInsumosQuimicos.Actualizar(codPartidaEngomado, codigoInsumo, 2, 0)
            Else
                partidaEngomadoDInsumosQuimicos.Actualizar(codPartidaEngomado, codigoInsumo, 2, preparacion2)
            End If
            If preparacion3 = "" Then
                partidaEngomadoDInsumosQuimicos.Actualizar(codPartidaEngomado, codigoInsumo, 3, 0)
            Else
                partidaEngomadoDInsumosQuimicos.Actualizar(codPartidaEngomado, codigoInsumo, 3, preparacion3)
            End If
            If preparacion4 = "" Then
                partidaEngomadoDInsumosQuimicos.Actualizar(codPartidaEngomado, codigoInsumo, 4, 0)
            Else
                partidaEngomadoDInsumosQuimicos.Actualizar(codPartidaEngomado, codigoInsumo, 4, preparacion4)
            End If
            If preparacion5 = "" Then
                partidaEngomadoDInsumosQuimicos.Actualizar(codPartidaEngomado, codigoInsumo, 5, 0)
            Else
                partidaEngomadoDInsumosQuimicos.Actualizar(codPartidaEngomado, codigoInsumo, 5, preparacion5)
            End If
            If preparacion6 = "" Then
                partidaEngomadoDInsumosQuimicos.Actualizar(codPartidaEngomado, codigoInsumo, 6, 0)
            Else
                partidaEngomadoDInsumosQuimicos.Actualizar(codPartidaEngomado, codigoInsumo, 6, preparacion6)
            End If
        End Sub

        Public Sub InsumosQuimicos_Inicializar()
            Dim formulacion As New NM_Formulacion
            Dim dt As DataTable
            Dim dr As DataRow

            dt = formulacion.ListarInsumosQuimicos(codUrdidoEngomado, Me.revEngomado, COD_FASE, "ENGCRU")
            For Each dr In dt.Rows
                InsumosQuimicos_Agregar(dr("codigo_insumo_quimico"), 0, 0, 0, 0, 0, 0)
            Next
        End Sub

        Public Function ListarIQ(ByVal codigoPartidaEngomado As String)
            Dim strSQL As String = "EXEC SP_NMIQPartidaEngomado '" & codigoPartidaEngomado & "'"
            Return BD.Query(strSQL)
        End Function

        Public Function ListAll() As DataTable
            Dim sql As String
            Dim objGen As New NM_Consulta
            sql = "Select * " & _
            " from NM_PartidaEngomadoYTED where tipo=" & COD_FASE & " and flagestado=1 "
            Return objGen.Query(sql)
        End Function

        Public Function List() As DataTable
            Dim sql As String
            Dim objGen As New NM_Consulta
            sql = "Select * from NM_PartidaEngomadoYTED " & _
            " where codigo_partida_engomadoted not like '____TE%' "
            Return objGen.Query(sql)
        End Function

        'funcion que lista las partidas que deben cerrarse para que pueda aperturarse otra
        'para el caso de engomado crudo solo es 1, solicitado x JCALDERON
        Public Function ListaPartidasxCerrar(ByRef pdtb_lista As DataTable, ByVal pint_tipoconsulta As Int16) As Boolean
            Dim lbln_fncestado As Boolean = False
            Try
                Dim objParametros() As Object = {"pchr_tipopartida", "CRU", _
                                                 "ptin_tipoconsulta", pint_tipoconsulta}
                pdtb_lista = _objConn.ObtenerDataTable("usp_tej_listapartidasxcerrar", objParametros)
                lbln_fncestado = True
            Catch Ex As Exception
                Throw Ex
            End Try
            Return lbln_fncestado
        End Function

        '-----------------------------------------------------------
        'Autor: Alexander Torres cardenas
        'Fecha: Junio 2014
        'Modificado: Implementar el engomado crudo en el proceso TED
        '-----------------------------------------------------------
        Public Sub ActualizarFechas_PE(ByVal pcodigoPartidaEngomado As String)
            Dim ltmp1 As String = ""
            Dim Conexion As AccesoDatosSQLServer
            Try
                Dim objParametro() As Object = {"codigo_partida_engomadoted", pcodigoPartidaEngomado}

                Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Conexion.EjecutarComando("usp_NM_PartidaEngomadoDProduccion_ActtualizarFechas", objParametro)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
    End Class
End Namespace