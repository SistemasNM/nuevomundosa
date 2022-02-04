'Option Strict On
Imports NM_General.NM_BaseDatos

Imports NM.AccesoDatos

Namespace NM_Tejeduria

    Public Class NM_DatosRecetaTED

        Implements IDisposable

        Public litSodaRecicla As Double    'Litros_Soda_Reciclada
        Public BeSodaRecicla As Double  'be_soda_reciclada
        Public litSoda50Be As Double    'litros_soda_50be
        Public litInicio As Double    'litros_inicio
        Public litFinal As Double    'litros_final
        Public litPreparados As Double   'litros_preparados
        Public UsuarioCrea As String    'usuario_creacion
        Public fechaCrea As Date    'fecha_creacion
        Public UsuarioMod As String    'usuario_modificacion
        Public fechaMod As Date    'fecha_modificacion
        Public codPartEngomadoTED As String    'codigo_partida_engomadoted  LLAVE PRIMARIA
        Public codReceta As String    'codigo_receta  LLAVE PRIMARIA
        Public revReceta As Integer   'revision_receta LLAVE PRIMARIA
        Public codFase As Integer    'codigo_fase LLAVE PRIMARIA
        Public eng_veces As Double
        Public eng_kg As Double
        Public Cod_IQ1 As String
        Public Cod_IQ2 As String
        Public Cod_IQ3 As String
        Public Cantidad_IQ1 As Double
        Public Cantidad_IQ2 As Double
        Public Cantidad_IQ3 As Double
        Public Tabla As New DataTable
        Public dtIQPret As New DataTable
        Public dtIQTenido As New DataTable
        Public dtIQEngomado As New DataTable

        Public dblGBe As Double

        Public num_Item As Integer

        Private m_sqlDtAccProduccion As AccesoDatosSQLServer

        Public Sub New()
            litSodaRecicla = 0
            BeSodaRecicla = 0
            litSoda50Be = 0
            litInicio = 0
            litFinal = 0
            litPreparados = 0
            UsuarioCrea = 0
            UsuarioMod = ""
            codPartEngomadoTED = ""
            codReceta = ""
            revReceta = 0
            codFase = 0
            eng_veces = 0
            eng_kg = 0
            Cod_IQ1 = ""
            Cod_IQ2 = ""
            Cod_IQ3 = ""
            Cantidad_IQ1 = 0
            Cantidad_IQ2 = 0
            Cantidad_IQ3 = 0
            num_Item = 0
            dblGBe = 0
            m_sqlDtAccProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)

        End Sub

        Const PRETRATAMIENTO As Integer = 1
        Const TENIDO As Integer = 2
        Const ENGOMADO As Integer = 3

#Region "metods para manipular la tabla NM_DatosREcetaTED"
        'Se realizo el cambio!
        Public Function Insertar() As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim fila As DataRow
            Try
                sql = "INSERT INTO NM_DatosREcetaTED (codigo_partida_engomadoted, " & _
                "codigo_receta,revision_receta, num_Item, codigo_fase," & _
                "Litros_Soda_Reciclada,be_soda_reciclada,litros_soda_50be," & _
                "litros_inicio,litros_final,litros_preparados,eng_veces," & _
                "eng_kg, cantidad_iq1, cantidad_iq2, cantidad_iq3, codigo_iq1, " & _
                "codigo_iq2, codigo_iq3, codigo_area,usuario_creacion,fecha_creacion)" & _
                " values ('" & codPartEngomadoTED & "','" & codReceta & "'," & _
                revReceta & "," & Me.num_Item & ",'" & codFase & "'," & litSodaRecicla & "," & _
                BeSodaRecicla & "," & litSoda50Be & "," & litInicio & "," & _
                litFinal & "," & litPreparados & "," & eng_veces & "," & _
                eng_kg & "," & Me.Cantidad_IQ1 & "," & Me.Cantidad_IQ2 & _
                "," & Cantidad_IQ3 & ", '" & Me.Cod_IQ1 & "','" & Me.Cod_IQ2 & "','" & _
                Cod_IQ3 & "','ENGTED', '" & UsuarioCrea & "',getdate())"
                Return objGen.Execute(sql)
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function Insertar2() As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim fila As DataRow
            Try
                Insertar2 = False
                Dim intEjecuto As Integer
                Dim dtInserto As New DataTable
                Dim objparametros As Object() = {"p_var_codigo_partida_engomadoted", codPartEngomadoTED, _
                                                 "p_var_codigo_receta", codReceta, _
                                                 "p_num_revision_receta", revReceta, _
                                                 "p_num_item", Me.num_Item, _
                                                 "p_num_codigo_fase", codFase, _
                                                 "p_num_Litros_soda_reciclada", litSodaRecicla, _
                                                 "p_num_be_soda_reciclada", BeSodaRecicla, _
                                                 "p_num_Litros_soda_50be", litSoda50Be, _
                                                 "p_num_Litros_inicio", litInicio, _
                                                 "p_num_Litros_final", litFinal, _
                                                 "p_num_Litros_preparados", litPreparados, _
                                                 "p_num_eng_veces", eng_veces, _
                                                 "p_num_eng_kg", eng_kg, _
                                                 "p_num_cantidad_iq1", Me.Cantidad_IQ1, _
                                                 "p_num_cantidad_iq2", Me.Cantidad_IQ2, _
                                                 "p_num_cantidad_iq3", Cantidad_IQ3, _
                                                 "p_num_codigo_iq1", Me.Cod_IQ1, _
                                                 "p_num_codigo_iq2", Me.Cod_IQ2, _
                                                 "p_num_codigo_iq3", Me.Cod_IQ3, _
                                                 "p_var_codigo_area", "ENGTED", _
                                                 "p_var_usuario", UsuarioCrea}

                'intEjecuto = m_sqlDtAccProduccion.EjecutarComando("usp_Ins_NM_DatosRecetaTED", objparametros)
                dtInserto = m_sqlDtAccProduccion.ObtenerDataTable("usp_Ins_NM_DatosRecetaTED", objparametros)

                If dtInserto.Rows.Count <> 0 Then
                    If dtInserto.Rows(0)("columna") = 1 Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                        Return False
                End If

            Catch ex As Exception
                Return False
                Throw ex
            End Try
        End Function

        Public Function eliminar(ByVal pCodigo_partida_engomadoted As String, ByVal pCodigo_receta As String, ByVal prevision_receta As Integer, ByVal pCodigo_fase As Integer) As Double
            Dim objGen As New NM_Consulta
            Dim strsql As String
            Try
                If pCodigo_partida_engomadoted <> "" AndAlso pCodigo_receta <> "" Then
                    Dim objRecetaDetTED As New NM_RecetaDetTED
                    strsql = "DELETE FROM NM_DatosREcetaTED WHERE "
                    strsql = strsql & "codigo_partida_engomadoted = '" & pCodigo_partida_engomadoted & "' and "
                    strsql = strsql & "codigo_receta = '" & pCodigo_receta & "' and "
                    strsql = strsql & "revision_receta = " & prevision_receta & " and "
                    strsql = strsql & "codigo_fase = " & pCodigo_fase & _
                    " and codigo_area = 'ENGTED' "
                    'objRecetaDetTED.Eliminar(pCodigo_partida_engomadoted, pCodigo_receta, prevision_receta, pCodigo_fase)
                    Return objGen.Execute(strsql)
                End If
            Catch
                Return False
            End Try
        End Function

        Public Sub Eliminar2()
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim fila As DataRow
            Try
                Dim objparametros As Object() = {"p_var_codigo_partida_engomadoted", codPartEngomadoTED, _
                                                 "p_var_codigo_receta", codReceta, _
                                                 "p_num_revision_receta", revReceta, _
                                                 "p_num_item", Me.num_Item, _
                                                 "p_num_codigo_fase", codFase, _
                                                 "p_var_codigo_area", "ENGTED"}

                m_sqlDtAccProduccion.EjecutarComando("usp_Del_NM_DatosRecetaTED", objparametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Function Update() As Boolean
            Dim objConn As New NM_Consulta
            Dim sql As String
            sql = "UPDATE NM_DatosREcetaTED SET " & _
            " litros_soda_reciclada=" & Me.litSodaRecicla & ", " & _
            " be_soda_reciclada = " & Me.BeSodaRecicla & ", " & _
            " litros_soda_50be = " & Me.litSoda50Be & ", " & _
            " litros_inicio = " & Me.litInicio & ", " & _
            " litros_final = " & Me.litFinal & ", " & _
            " litros_preparados = " & Me.litPreparados & ", " & _
            " eng_veces = " & Me.eng_veces & ", " & _
            " eng_kg = " & Me.eng_kg & ", " & _
            " codigo_iq1 = '" & Me.Cod_IQ1 & "', " & _
            " codigo_iq2 = '" & Me.Cod_IQ2 & "', " & _
            " codigo_iq3 = '" & Me.Cod_IQ3 & "', " & _
            " cantidad_iq1 = " & Me.Cantidad_IQ1 & ", " & _
            " cantidad_iq2 = " & Me.Cantidad_IQ2 & ", " & _
            " cantidad_iq3 = " & Me.Cantidad_IQ3 & " " & _
            " where codigo_partida_engomadoted='" & Me.codPartEngomadoTED & _
            "' and codigo_receta = '" & Me.codReceta & "' " & _
            " and revision_receta = " & Me.revReceta & _
            " and codigo_fase='" & Me.codFase & "' " & _
            " and codigo_area = 'ENGTED' "

            Return objConn.Execute(sql)
        End Function

        Public Sub Update2()
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim fila As DataRow
            Try

                Dim objparametros As Object() = {"p_var_codigo_partida_engomadoted", codPartEngomadoTED, _
                                                 "p_var_codigo_receta", codReceta, _
                                                 "p_num_revision_receta", revReceta, _
                                                 "p_num_item", Me.num_Item, _
                                                 "p_num_codigo_fase", codFase, _
                                                 "p_num_Litros_soda_reciclada", litSodaRecicla, _
                                                 "p_num_be_soda_reciclada", BeSodaRecicla, _
                                                 "p_num_Litros_soda_50be", litSoda50Be, _
                                                 "p_num_Litros_inicio", litInicio, _
                                                 "p_num_Litros_final", litFinal, _
                                                 "p_num_Litros_preparados", litPreparados, _
                                                 "p_num_eng_veces", eng_veces, _
                                                 "p_num_eng_kg", eng_kg, _
                                                 "p_num_cantidad_iq1", Me.Cantidad_IQ1, _
                                                 "p_num_cantidad_iq2", Me.Cantidad_IQ2, _
                                                 "p_num_cantidad_iq3", Cantidad_IQ3, _
                                                 "p_num_codigo_iq1", Me.Cod_IQ1, _
                                                 "p_num_codigo_iq2", Me.Cod_IQ2, _
                                                 "p_num_codigo_iq3", Me.Cod_IQ3, _
                                                 "p_var_codigo_area", "ENGTED", _
                                                 "p_var_usuario", UsuarioCrea}

                m_sqlDtAccProduccion.EjecutarComando("usp_Upd_NM_DatosRecetaTED", objparametros)

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub actualizar(ByVal pCodigo_partida_engomadoted As String, ByVal pCodigo_receta As String, ByVal srevision_receta As Integer, ByVal pcod_fase As Integer, _
      ByVal pLitros_Soda_Reciclada As Double, ByVal pbe_soda_reciclada As Double, ByVal plitros_soda_50be As Double, _
      ByVal plitros_inicio As String, ByVal pLitros_final As String, ByVal plitros_preparados _
      As String, Optional ByVal pveces As Double = 0.0, Optional ByVal pkg As Double = 0.0, Optional ByVal pIQs As DataTable = Nothing)
            Dim objGen As New NM_Consulta
            Dim strsql As String
            Dim fila As DataRow
            strsql = "UPDATE NM_DatosREcetaTED SET "
            Dim commandString As New System.Text.StringBuilder
            commandString.Append(strsql)
            If pcod_fase = PRETRATAMIENTO Then
                commandString.Append("Litros_Soda_Reciclada = " & pLitros_Soda_Reciclada & ",")
                commandString.Append("be_soda_reciclada = " & pbe_soda_reciclada & ",")
                commandString.Append("litros_soda_50be= " & plitros_soda_50be & ",")
            End If
            If pcod_fase = TENIDO Then
                commandString.Append("litros_inicio = " & plitros_inicio & ",")
                commandString.Append("litros_preparados = " & plitros_preparados & ",")
                commandString.Append("litros_final= " & pLitros_final & ",")
                commandString.Append("cantidad_iq1 = " & Cantidad_IQ1 & ",")
                commandString.Append("cantidad_iq2 = " & Cantidad_IQ2 & ",")
            End If
            If pcod_fase = ENGOMADO Then
                commandString.Append("eng_veces = " & pveces & ",")
                commandString.Append("eng_kg = " & pkg & ",")
                commandString.Append("cantidad_iq3 = " & Cantidad_IQ3 & ",")
            End If
            commandString.Append("usuario_modificacion='" & UsuarioMod & "',")
            commandString.Append("fecha_modificacion=GETDATE()")
            commandString.Append(" where codigo_partida_engomadoted = '" & pCodigo_partida_engomadoted & "' and ")
            commandString.Append("codigo_receta = '" & pCodigo_receta & "' and ")
            commandString.Append("revision_receta = " & srevision_receta & " and ")
            commandString.Append("codigo_fase = '" & pcod_fase & "'")
            Try
                objGen.Query(commandString.ToString)
                Dim objRecetadetTED As New NM_RecetaDetTED
                ' actualiza los detalles con la tabla dada como parametro
                If Not IsNothing(pIQs) Then
                    For Each fila In pIQs.Rows
                        If fila("codigo_insumo_quimico") <> "" And fila("cantidad") >= 0 Then
                            objRecetadetTED.actualizar(fila("codigo_insumo_quimico"), pCodigo_partida_engomadoted, pCodigo_receta, srevision_receta, _
                            pcod_fase, fila("cantidad"), "dbo")
                        End If
                    Next
                End If
            Catch
                Throw
            End Try
        End Sub
#End Region

        Public Sub InsertarDatosReceta(ByVal pCodigo_partida_engomadoted As String, ByVal scodFormulacionTed As String, ByVal srevFormulacionTEd As Integer, ByVal pcod_Fase As Integer)
            Dim Formulacion As New NM_Formulacion
            codPartEngomadoTED = pCodigo_partida_engomadoted
            '----obtiene el codigo de receta y revision que estan asociados al codigo de formulacion, revision(codigo ted y revision ted) y la fase
            ' Throw New Exception("[" & scodFormulacionTed & "],[" & pcod_Fase & "],[" & srevFormulacionTEd)
            Formulacion.getRecetas(scodFormulacionTed, pcod_Fase, srevFormulacionTEd, "ENGTED")
            revReceta = Formulacion.revision_receta
            codReceta = Formulacion.codigo_receta
            '--------------------------------------------------------------------
            codFase = pcod_Fase
            '--Registra los datos de receta (en la tabla NM_DatosRecetaTED) con los atributs asignado
            Insertar()
            '--Registra los detalles en la tabla NM_RecetaDetTEd
            FillDetalleReceta(pCodigo_partida_engomadoted, scodFormulacionTed, srevFormulacionTEd, pcod_Fase)
        End Sub

        Private Sub FillDetalleReceta(ByVal pCodigo_partida_engomadoted As String, ByVal scodFormulacionTed As String, ByVal srevFormulacionTed As Integer, ByVal pcod_Fase As Integer)
            Dim tabla As New DataTable
            Dim fila As DataRow
            Dim objformulacion As New NM_Formulacion
            Dim objDetTED As New NM_RecetaDetTED
            '--Obtiene la lista de insumos quimicos que pertencen a un codigo y revision de formulación
            '--y se graba en la Tabla NM_NM_RecetaDetTEd
            'Throw New Exception("[" & scodFormulacionTed & "],[" & srevFormulacionTed & "],[" & pcod_Fase)
            tabla = objformulacion.ListarInsumosQuimicos(scodFormulacionTed, srevFormulacionTed, pcod_Fase)
            For Each fila In tabla.Rows
                objDetTED.cantidad = 0
                objDetTED.codFase = pcod_Fase
                objDetTED.fechaCrea = Date.Today
                objDetTED.fechaMod = Date.Today
                objDetTED.codInsumoQuimico = Trim(fila("codigo_insumoquimico"))
                objDetTED.codPartidaEngomadoTed = pCodigo_partida_engomadoted
                objDetTED.revReceta = Trim(fila("revision_receta"))
                objDetTED.codReceta = Trim(fila("codigo_receta"))
                objDetTED.insertar()
            Next
        End Sub

        Public Sub seekDatosReceta(ByVal pCodigo_partida_engomadoted As String, ByVal pCodigo_receta As String, ByVal srevision_receta As Integer, ByVal pCodigo_fase As Integer)
            Dim fila As DataRow
            Dim objGen As New NM_Consulta
            Dim strsql As String
            strsql = "SELECT * FROM NM_DatosREcetaTED WHERE "
            strsql = strsql & "codigo_partida_engomadoted = '" & pCodigo_partida_engomadoted & "' and "
            strsql = strsql & "codigo_receta = '" & pCodigo_receta & "' and "
            strsql = strsql & "revision_receta = " & srevision_receta & " and "
            strsql = strsql & "codigo_fase = '" & pCodigo_fase & "'"
            Dim tabla As New DataTable
            tabla = objGen.Query(strsql)
            For Each fila In tabla.Rows
                If Not IsDBNull(fila("Litros_Soda_Reciclada")) Then litSodaRecicla = fila("Litros_Soda_Reciclada")
                If Not IsDBNull(fila("be_soda_reciclada")) Then BeSodaRecicla = fila("be_soda_reciclada")
                If Not IsDBNull(fila("litros_soda_50be")) Then litSoda50Be = fila("litros_soda_50be")
                If Not IsDBNull(fila("litros_inicio")) Then litInicio = fila("litros_inicio")
                If Not IsDBNull(fila("litros_final")) Then litFinal = fila("litros_final")
                If Not IsDBNull(fila("litros_preparados")) Then litPreparados = fila("litros_preparados")
                If Not IsDBNull(fila("usuario_creacion")) Then UsuarioCrea = fila("usuario_creacion")
                If Not IsDBNull(fila("fecha_creacion")) Then fechaCrea = fila("fecha_creacion")
                If Not IsDBNull(fila("usuario_modificacion")) Then UsuarioMod = fila("usuario_modificacion")
                If Not IsDBNull(fila("fecha_modificacion")) Then fechaMod = fila("fecha_modificacion")
                If Not IsDBNull(fila("codigo_partida_engomadoted")) Then codPartEngomadoTED = fila("codigo_partida_engomadoted")
                If Not IsDBNull(fila("codigo_receta")) Then codReceta = fila("codigo_receta")
                If Not IsDBNull(fila("revision_receta")) Then revReceta = fila("revision_receta")
                If Not IsDBNull(fila("eng_veces")) Then eng_veces = fila("eng_veces")
                If Not IsDBNull(fila("eng_kg")) Then eng_kg = fila("eng_kg")
                '   If Not IsDBNull(fila("revision_receta")) Then revReceta = fila("revision_receta")
                If Not IsDBNull(fila("codigo_fase")) Then codFase = fila("codigo_fase")
                Exit For
            Next
            If pCodigo_fase = PRETRATAMIENTO Then
                dtIQPret = loadDetallePretramiento(pCodigo_partida_engomadoted, revReceta, pCodigo_receta)
            End If
            If pCodigo_fase = TENIDO Then
                dtIQTenido = loadDetalleTenido(pCodigo_partida_engomadoted, revReceta, pCodigo_receta)
            End If
            If pCodigo_fase = ENGOMADO Then
                dtIQEngomado = loadDetalleEngomado(pCodigo_partida_engomadoted, revReceta, pCodigo_receta)
            End If
        End Sub
        Public Sub seekDatosReceta(ByVal pCodigo_partida_engomadoted As String, ByVal pCodigo_fase As Integer)
            Dim fila As DataRow
            Dim objGen As New NM_Consulta
            Dim strsql As String
            strsql = "SELECT * FROM NM_DatosREcetaTED WHERE "
            strsql = strsql & "codigo_partida_engomadoted = '" & pCodigo_partida_engomadoted & "' and "
            strsql = strsql & "codigo_fase = '" & pCodigo_fase & "' and codigo_area = 'ENGTED' "
            Dim tabla As New DataTable
            tabla = objGen.Query(strsql)
            For Each fila In tabla.Rows
                If Not IsDBNull(fila("Litros_Soda_Reciclada")) Then litSodaRecicla = fila("Litros_Soda_Reciclada")
                If Not IsDBNull(fila("be_soda_reciclada")) Then BeSodaRecicla = fila("be_soda_reciclada")
                If Not IsDBNull(fila("litros_soda_50be")) Then litSoda50Be = fila("litros_soda_50be")
                If Not IsDBNull(fila("litros_inicio")) Then litInicio = fila("litros_inicio")
                If Not IsDBNull(fila("litros_final")) Then litFinal = fila("litros_final")
                If Not IsDBNull(fila("litros_preparados")) Then litPreparados = fila("litros_preparados")
                If Not IsDBNull(fila("usuario_creacion")) Then UsuarioCrea = fila("usuario_creacion")
                If Not IsDBNull(fila("fecha_creacion")) Then fechaCrea = fila("fecha_creacion")
                If Not IsDBNull(fila("usuario_modificacion")) Then UsuarioMod = fila("usuario_modificacion")
                If Not IsDBNull(fila("fecha_modificacion")) Then fechaMod = fila("fecha_modificacion")
                If Not IsDBNull(fila("codigo_partida_engomadoted")) Then codPartEngomadoTED = fila("codigo_partida_engomadoted")
                If Not IsDBNull(fila("codigo_receta")) Then codReceta = fila("codigo_receta")
                If Not IsDBNull(fila("revision_receta")) Then revReceta = fila("revision_receta")
                If Not IsDBNull(fila("eng_veces")) Then eng_veces = fila("eng_veces")
                If Not IsDBNull(fila("eng_kg")) Then eng_kg = fila("eng_kg")
                '   If Not IsDBNull(fila("revision_receta")) Then revReceta = fila("revision_receta")
                If Not IsDBNull(fila("codigo_fase")) Then codFase = fila("codigo_fase")
                Exit For
            Next
            If pCodigo_fase = PRETRATAMIENTO Then
                dtIQPret = loadDetallePretramiento(pCodigo_partida_engomadoted, revReceta, codReceta)
            End If
            If pCodigo_fase = TENIDO Then
                dtIQTenido = loadDetalleTenido(pCodigo_partida_engomadoted, revReceta, codReceta)
            End If
            If pCodigo_fase = ENGOMADO Then
                dtIQEngomado = loadDetalleEngomado(pCodigo_partida_engomadoted, revReceta, codReceta)
            End If
        End Sub

#Region "Metodos para devolver los insumos quimicos q corresponden a una fase"

        Private Function loadDetalle(ByVal pCodigo_partida_engomadoted As String, ByVal srevision_receta As Integer, ByVal pCodigo_receta As String, ByVal fase As Integer) As DataTable
            Dim objGen As New NM_Consulta
            Dim strsql As String
            strsql = "SELECT distinct * FROM NM_REcetaDetTED WHERE "
            strsql = strsql & "codigo_partida_engomadoted = '" & pCodigo_partida_engomadoted & "' and "
            strsql = strsql & "codigo_receta = '" & pCodigo_receta & "' and "
            strsql = strsql & "revision_receta = " & srevision_receta & " and "
            strsql = strsql & "codigo_fase = '" & fase & "' and codigo_area = 'ENGTED' "
            Try
                Return objGen.Query(strsql)
            Catch
            End Try
        End Function

        Public Function loadDetallePretramiento(ByVal pCodigo_partida_engomadoted As String, ByVal sRevision_receta As Integer, ByVal pCodigo_receta As String) As DataTable
            Return loadDetalle(pCodigo_partida_engomadoted, sRevision_receta, pCodigo_receta, PRETRATAMIENTO)
        End Function

        Public Function loadDetalleTenido(ByVal pCodigo_partida_engomadoted As String, ByVal sRevision_receta As Integer, ByVal pCodigo_receta As String) As DataTable
            Return loadDetalle(pCodigo_partida_engomadoted, sRevision_receta, pCodigo_receta, TENIDO)
        End Function

        Public Function loadDetalleEngomado(ByVal pCodigo_partida_engomadoted As String, ByVal sRevision_receta As Integer, ByVal pCodigo_receta As String) As DataTable
            Return loadDetalle(pCodigo_partida_engomadoted, sRevision_receta, pCodigo_receta, ENGOMADO)
        End Function

        Public Function List(ByVal sCodigoPartidaEngomado As String, _
        ByVal sFase As String, ByVal sArea As String) As DataTable
            Dim sql As String, objConn As New NM_Consulta
            Dim dt As New DataTable, nFase As Integer
            If sFase = "ENGOMADO" Then
                nFase = 3
            ElseIf sFase = "TENIDO" Then
                nFase = 2
            ElseIf sFase = "PRETRATAMIENTO" Then
                nFase = 1
            End If

            sql = "select distinct DT.* " & _
            " from NM_DatosRecetaTED DT, NM_Receta R " & _
            " where dt.codigo_receta = R.codigo_receta " & _
            " and Dt.revision_receta = R.revision_receta " & _
            " and DT.codigo_fase = " & nFase & _
            " and R.codigo_area ='" & sArea & "' " & _
            " and DT.codigo_partida_engomadoted='" & sCodigoPartidaEngomado & "' "

            dt = objConn.Query(sql)
            Return dt
        End Function

        Public Function List2(ByVal sCodigoPartidaEngomado As String, _
                ByVal sFase As String, ByVal sArea As String) As DataTable
            Try
                Dim sql As String, objConn As New NM_Consulta
                Dim dt As New DataTable, nFase As Integer
                If sFase = "ENGOMADO" Then
                    nFase = 3
                ElseIf sFase = "TENIDO" Then
                    nFase = 2
                ElseIf sFase = "PRETRATAMIENTO" Then
                    nFase = 1
                End If
                Dim objParametros() As Object = {"p_var_codigo_partida_engomadoted", sCodigoPartidaEngomado, _
                                                 "p_num_codigo_fase", nFase, _
                                                 "p_var_codigo_area", sArea}
                dt = m_sqlDtAccProduccion.ObtenerDataTable("usp_qry_ObtenerDRecetaTED", objParametros)

                Return dt

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function get_Be(ByVal dblBe As Double) As Double
            Try
                Dim sql As String, objConn As New NM_Consulta
                Dim dt As New DataTable
                Dim dblKgSoda As Double
                dblKgSoda = 0
                Dim objParametros() As Object = {"p_num_SodaBe", dblBe}
                dt = m_sqlDtAccProduccion.ObtenerDataTable("usp_get_ObtenerBe", objParametros)
                If dt.Rows.Count <> 0 Then
                    dblKgSoda = dt.Rows(0)("kg_soda_reciclada")
                    dblGBe = dblKgSoda
                End If
                Return dblKgSoda
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function get_numItem(ByVal sCodigoPartidaEngomado As String, _
                ByVal nFase As Integer, ByVal sArea As String, ByVal sCodigoReceta As String, _
                ByVal intRevisionReceta As Integer) As Integer
            Try
                Dim sql As String, objConn As New NM_Consulta
                Dim dt As New DataTable
                Dim int_numItem As Integer
                int_numItem = 0

                Dim objParametros() As Object = {"p_var_codigo_partida_engomadoted", sCodigoPartidaEngomado, _
                                                 "p_num_codigo_fase", nFase, _
                                                 "p_var_codigo_area", sArea, _
                                                 "p_var_codigo_receta", sCodigoReceta, _
                                                 "p_num_revision_receta", intRevisionReceta}

                dt = m_sqlDtAccProduccion.ObtenerDataTable("usp_get_numItem", objParametros)
                If dt.Rows.Count <> 0 Then
                    int_numItem = dt.Rows(0)("num_Item")
                End If
                Return int_numItem
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

        'falta modificar la revision de receta
#Region "Metodos de calculo de las cantidades ingresadas en la pantalla fciq"
        Public Function CalcularRecetaTenido(ByVal pcod_Receta As String, ByVal srevision_receta As Integer, ByVal pLitInicio As Double, ByVal pLitPrep As Double, ByVal pLitFinal As Double) As DataTable
            Dim fila As DataRow
            Dim objformulacion As New NM_Formulacion
            Dim IQsTenido As New DataTable
            Dim factor As Double = 0
            If pLitInicio >= 0 And pLitPrep >= 0 And pLitFinal >= 0 Then
                factor = pLitInicio + pLitPrep - pLitFinal
                ' se le agrego la revision de receta
                IQsTenido = objformulacion.ListarInsumosQuimicos(pcod_Receta, srevision_receta, 2) 'obtienes todos los insumosquimicos para la fase de teñido    GetIQs(codTEd, revTED, 2)
                If Not IQsTenido.Columns.Contains("cantidad") Then ' verifica si existe la columna cantidad
                    IQsTenido.Columns.Add("cantidad", Type.GetType("System.Double"))
                End If
                For Each fila In IQsTenido.Rows
                    If Not IsDBNull(fila("Concentracion")) Then
                        fila("cantidad") = factor * fila("Concentracion") / 1000
                    Else
                        fila("cantidad") = 0.0
                    End If
                Next
                Return IQsTenido
            Else
                Return Nothing
            End If
        End Function

        Public Function CalcularRecetaTenido(ByVal pcod_Receta As String, ByVal srevision_receta As Integer, ByVal pLitInicio As Double, ByVal pLitPrep As Double, ByVal pLitFinal As Double, ByVal dtIQs As DataTable) As DataTable
            Dim fila As DataRow
            Dim RecetaIQ As New NM_RecetaInsumoQuimico
            Dim factor As Double = 0
            If pLitInicio >= 0 And pLitPrep >= 0 And pLitFinal >= 0 Then
                factor = pLitInicio + pLitPrep - pLitFinal
                ' se le agrego la revision de receta
                If Not dtIQs.Columns.Contains("cantidad") Then ' verifica si existe la columna cantidad
                    dtIQs.Columns.Add("cantidad", Type.GetType("System.Double"))
                End If
                For Each fila In dtIQs.Rows
                    RecetaIQ.Seek(pcod_Receta, srevision_receta, fila("codigo_insumo_quimico"), "ENGTED")
                    If Not IsDBNull(RecetaIQ.concentracion) Then
                        fila("cantidad") = factor * RecetaIQ.concentracion / 1000
                    Else
                        fila("cantidad") = 0.0
                    End If
                Next
                Return dtIQs
            Else
                Return Nothing
            End If
        End Function

        Public Function CalcularRecetaEngomado(ByVal pcod_receta As String, ByVal srevision_receta As Integer, ByVal Veces As Integer, ByVal Kg As Double) As DataTable
            Dim fila As DataRow
            Dim objFormulacion As New NM_Formulacion
            Dim IQsEngomado As New DataTable
            Dim factor As Double = 0
            Dim suma As Double = 0
            If Veces >= 0 And Kg >= 0 Then
                factor = Veces * Kg
                IQsEngomado = objFormulacion.ListarInsumosQuimicos(pcod_receta, srevision_receta, 3) 'Obtiene los insumos quimicos para la fase de engomado  GetIQs(codTEd, revTED, 3)
                '--- verifica si existe la columna cantidad, si no existe la crea
                If Not IQsEngomado.Columns.Contains("Cantidad") Then
                    IQsEngomado.Columns.Add("Cantidad", Type.GetType("System.Double"))
                End If
                '-------------------------------------------
                For Each fila In IQsEngomado.Rows
                    If Not IsDBNull(fila("Concentracion")) Then
                        fila("Cantidad") = factor * fila("Concentracion") / 1000
                    Else
                        fila("Cantidad") = 0.0
                    End If
                Next
                Return IQsEngomado
            Else
                Return Nothing
            End If
        End Function

        Public Function CalcularRecetaEngomado(ByVal pcod_receta As String, ByVal srevision_receta As Integer, ByVal Veces As Integer, ByVal Kg As Double, ByVal dtIQs As DataTable) As DataTable
            Dim fila As DataRow
            Dim RecetaIQ As New NM_RecetaInsumoQuimico
            Dim factor As Double = 0
            If Veces >= 0 And Kg >= 0 Then
                factor = Veces * Kg
                '--- verifica si existe la columna cantidad, si no existe la crea
                If Not dtIQs.Columns.Contains("Cantidad") Then
                    dtIQs.Columns.Add("Cantidad", Type.GetType("System.Double"))
                End If
                '-------------------------------------------
                For Each fila In dtIQs.Rows
                    RecetaIQ.Seek(pcod_receta, srevision_receta, fila("codigo_insumo_quimico"), "ENGTED")
                    If Not IsDBNull(RecetaIQ.concentracion) Then
                        fila("Cantidad") = factor * RecetaIQ.concentracion / 1000
                    Else
                        fila("Cantidad") = 0.0
                    End If
                Next
                Return dtIQs
            Else
                Return Nothing
            End If
        End Function

        Public Function GetRecetaX(ByVal codTed As String, ByVal revTEd As Integer, ByVal pcod_fase As Integer) As String
            Dim objformulacion As New NM_Formulacion
            Dim fila As DataRow
            Dim result As String
            Dim tabla As New DataTable
            Try
                tabla = objformulacion.ListarInsumosQuimicos(codTed, revTEd, pcod_fase)
                For Each fila In tabla.Rows
                    If Not IsDBNull(fila("codigo_receta")) Then
                        result = Trim((fila("codigo_receta")))
                    Else
                        Throw New Exception("Codigo_Receta sin valor asignado")
                    End If
                    Exit For
                Next
                Return result
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region


        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccProduccion.Dispose()
        End Sub

    End Class

End Namespace
