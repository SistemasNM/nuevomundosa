Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos
Imports System.Data.SqlClient

Namespace NM_Tejeduria
    Public Class NM_Urdimbre

#Region "Variable"
        Public codigo_urdimbre As String
        Public revision_urdimbre As String
        Public codigo_maquina As String
        Public velocidad As String
        Public numero_hilos As String
        Public posicion_fileta As String
        Public peso_metro_cuadrado As String
        Public tension As String
        Public presion_tambor As String
        Public peso_metro_lineal As String
        Public usuario_creacion As String
        Public fecha_creacion As String
        Public usuario_modificacion As String
        Public fecha_modificacion As String
        Public Detalles As DataTable
        Public Tejido As DataTable
        Public Trama As DataTable
        Public Orillo As DataTable
        Public flagestado As Integer
        Private objUtil As New NM_General.Util

        Private _objConexion As AccesoDatosSQLServer

#End Region


        Sub New()
            codigo_urdimbre = ""
            revision_urdimbre = "0"
            codigo_maquina = "0"
            velocidad = "0"
            numero_hilos = "0"
            posicion_fileta = "0"
            peso_metro_cuadrado = "0"
            tension = "0"
            usuario_creacion = ""
            presion_tambor = "0"
            fecha_creacion = ""
            peso_metro_lineal = "0"
            usuario_modificacion = ""
            fecha_modificacion = ""
        End Sub

        Sub New(ByVal txtcodigo_urdimbre As String)
            Seek(txtcodigo_urdimbre)
            Dim objDetalle As New NM_DetalleUrdimbre
            Dim objTrama As New NM_Trama

            Detalles = objDetalle.List(txtcodigo_urdimbre, revision_urdimbre)
            Tejido = objDetalle.ListByType(txtcodigo_urdimbre, "Tejido")
            Orillo = objDetalle.ListByType(txtcodigo_urdimbre, "Orillo")
            'Trama = objTrama.LoadDT(txtcodigo_urdimbre)    'La trama esta asociada a la tela

        End Sub

        Sub New(ByVal txtcodigo_urdimbre As String, ByVal bparagrid As Boolean)
            Seek(txtcodigo_urdimbre)
            Dim objDetalle As New NM_DetalleUrdimbre

            Detalles = objDetalle.loadDT(txtcodigo_urdimbre, revision_urdimbre)
            Trama = objDetalle.loadDT(txtcodigo_urdimbre, "Tejido", bparagrid)
            Tejido = objDetalle.loadDT(txtcodigo_urdimbre, "Tejido", bparagrid)
            Orillo = objDetalle.loadDT(txtcodigo_urdimbre, "Orillo", bparagrid)

        End Sub

        Function Exist(ByVal txtcodigo_urdimbre As String) As Boolean
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            sql = "Select * from NM_Urdimbre where codigo_urdimbre='" & txtcodigo_urdimbre & "' and flagestado = 1"
            '   Throw New Exception(sql)
            objDT = objGen.Query(sql)
            Return (objDT.Rows.Count > 0)
        End Function

        Sub Seek(ByVal txtcodigo_urdimbre As String)
            SeekByRevision(txtcodigo_urdimbre, 1)
        End Sub

        Sub SeekByRevision(ByVal txtcodigo_urdimbre As String, ByVal nFlagEstado As Integer)
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            sql = "Select * from NM_Urdimbre where codigo_urdimbre='" & txtcodigo_urdimbre & _
            "' and flagestado = " & nFlagEstado & _
            " order by revision_urdimbre "
            ' Throw New Exception(sql)
            objDT = objGen.Query(sql)
            Dim noreg As Boolean = True
            If Not objDT Is Nothing Then
                For Each objDR In objDT.Rows
                    If Not IsDBNull(objDR("codigo_urdimbre")) Then codigo_urdimbre = objDR("codigo_urdimbre")
                    If Not IsDBNull(objDR("revision_urdimbre")) Then revision_urdimbre = objDR("revision_urdimbre")
                    If Not IsDBNull(objDR("codigo_maquina")) Then codigo_maquina = objDR("codigo_maquina")
                    'If Not IsDBNull(objDR("velocidad")) Then velocidad = objDR("velocidad")
                    'If Not IsDBNull(objDR("posicion_fileta")) Then posicion_fileta = objDR("posicion_fileta")
                    'If Not IsDBNull(objDR("tension")) Then tension = objDR("tension")
                    'If Not IsDBNull(objDR("presion_tambor")) Then presion_tambor = objDR("presion_tambor")
                    noreg = False
                Next
            End If
            If noreg Then
                codigo_urdimbre = txtcodigo_urdimbre
                revision_urdimbre = "0"
                codigo_maquina = "0"
                velocidad = "0"
                posicion_fileta = "0"
                tension = "0"
                presion_tambor = "0"
            Else
                Dim objDetalle As New NM_DetalleUrdimbre
                Detalles = objDetalle.List(txtcodigo_urdimbre, revision_urdimbre)
                Trama = objDetalle.ListByType(txtcodigo_urdimbre, "Tejido")
                Tejido = objDetalle.ListByType(txtcodigo_urdimbre, "Tejido")
                Orillo = objDetalle.ListByType(txtcodigo_urdimbre, "Orillo")
            End If
        End Sub

        Public Function SendHistory(ByVal scodigo_urdimbre As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            sql = "Update NM_Urdimbre set flagestado=0 " & _
            " where codigo_urdimbre='" & scodigo_urdimbre & _
            "' AND flagestado=1 "
            Return objConn.Execute(sql)
        End Function

        Sub Seek_2(ByVal txtcodigo_urdimbre As String, ByVal revisionUrdimbre As Integer)
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            sql = "Select * from NM_Urdimbre where codigo_urdimbre='" & txtcodigo_urdimbre & _
            "' and revision_urdimbre = " & revisionUrdimbre & _
            " order by revision_urdimbre "

            objDT = objGen.Query(sql)
            Dim noreg As Boolean = True
            If Not objDT Is Nothing Then
                For Each objDR In objDT.Rows
                    If Not IsDBNull(objDR("codigo_urdimbre")) Then codigo_urdimbre = objDR("codigo_urdimbre")
                    If Not IsDBNull(objDR("revision_urdimbre")) Then revision_urdimbre = objDR("revision_urdimbre")
                    If Not IsDBNull(objDR("codigo_maquina")) Then codigo_maquina = objDR("codigo_maquina")
                    'If Not IsDBNull(objDR("velocidad")) Then velocidad = objDR("velocidad")
                    'If Not IsDBNull(objDR("posicion_fileta")) Then posicion_fileta = objDR("posicion_fileta")
                    'If Not IsDBNull(objDR("tension")) Then tension = objDR("tension")
                    'If Not IsDBNull(objDR("presion_tambor")) Then presion_tambor = objDR("presion_tambor")

                    Dim objDetalle As New NM_DetalleUrdimbre
                    Detalles = objDetalle.List(txtcodigo_urdimbre, revisionUrdimbre)
                    Trama = objDetalle.ListByType(txtcodigo_urdimbre, revisionUrdimbre, "Tejido")
                    Tejido = objDetalle.ListByType(txtcodigo_urdimbre, revisionUrdimbre, "Tejido")
                    Orillo = objDetalle.ListByType(txtcodigo_urdimbre, revisionUrdimbre, "Orillo")
                Next
            End If

        End Sub

        Function Seek(ByVal txtcodigo_urdimbre As String, ByVal Revision_Urdimbre As Integer) As DataTable
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            sql = "Select * from NM_Urdimbre where codigo_urdimbre='" & _
            txtcodigo_urdimbre & "' and revision_urdimbre=" & Revision_Urdimbre
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Function Add(ByVal CodigoUrdimbre As String, ByVal RevisionUrdimbre As Integer, _
         ByVal CodigoItem As Integer, ByVal Tipo As Integer, _
         ByVal CodigoHilo As String, ByVal NumeroHilos As Integer, _
         ByVal PesoM2 As Double, ByVal PesoMLineal As Double)

            Dim sql As String, codErr As Integer = 0, objTHilo As New NM_THilo
            Dim objTable As New DataTable, objGen As New NM_Consulta

            Try
                If CodigoUrdimbre <> "" AndAlso Val(RevisionUrdimbre) >= 0 AndAlso _
                Val(CodigoItem) >= 0 AndAlso Val(Tipo) >= 0 AndAlso CodigoHilo <> "" AndAlso _
                NumeroHilos > 0 AndAlso PesoM2 > 0 AndAlso PesoMLineal > 0 Then
                    objTable = objTHilo.Obtener(CodigoHilo)
                    If objTable.Rows.Count > 0 Then
                        sql = "Insert into NM_Urdimbre (" & _
                        "codigo_urdimbre, revision_urdimbre, codigo_item, tipo " & _
                        ",numero_hilos, peso_metro_cuadrado, peso_metro_lineal"
                        sql = sql & ") values('" & UCase(CodigoUrdimbre) & "'," & RevisionUrdimbre & _
                        "," & CodigoItem & "," & Tipo & "," & NumeroHilos & _
                        "," & PesoM2 & "," & PesoMLineal & ")"
                        codErr = objGen.Execute(sql)
                    Else
                        codErr = 0
                    End If
                Else
                    codErr = 0
                End If
            Catch ex As Exception
                codErr = 0
            End Try
        End Function

        Function Delete(ByVal Codigo As String, ByVal Revision As Integer) As Integer
            Dim sql As String, codErr As Integer = 0, objGen As New NM_Consulta
            Try
                If Codigo <> "" And Val(Revision) >= 0 Then
                    sql = "Delete from NM_Urdimbre where " & _
                    "codigo_urdimbre = '" & Codigo & "' and revision_urdimbre=" & Revision
                    codErr = objGen.Execute(sql)
                Else
                    codErr = 0
                End If
            Catch ex As Exception
                codErr = 0
            End Try
        End Function

        Function Update(ByVal CodigoUrdimbre As String, ByVal RevisionUrdimbre As Integer, _
         ByVal CodigoItem As Integer, ByVal Tipo As Integer, _
         ByVal CodigoHilo As String, ByVal NumeroHilos As Integer, _
         ByVal PesoM2 As Double, ByVal PesoMLineal As Double)

            Dim sql As String, codErr As Integer = 0, objTHilo As NM_THilo
            Dim objTable As New DataTable, objGen As New NM_Consulta

            Try
                If CodigoUrdimbre <> "" AndAlso Val(RevisionUrdimbre) >= 0 AndAlso _
                Val(CodigoItem) >= 0 AndAlso Val(Tipo) >= 0 AndAlso CodigoHilo <> "" AndAlso _
                NumeroHilos > 0 AndAlso PesoM2 > 0 AndAlso PesoMLineal > 0 Then
                    objTable = objTHilo.Obtener(CodigoHilo)
                    If objTable.Rows.Count > 0 Then
                        sql = "Update NM_Urdimbre set " & _
                        "codigo_urdimbre = '" & UCase(CodigoUrdimbre) & "', revision_urdimbre = " & RevisionUrdimbre & _
                        ", numero_hilos = " & NumeroHilos & _
                        ", peso_metro_cuadrado = " & PesoM2 & ", peso_metro_lineal = " & PesoMLineal & _
                        " where codigo_urdimbre = '" & CodigoUrdimbre & "' and revision_urdimbre = " & RevisionUrdimbre & " "
                        codErr = objGen.Execute(sql)
                    Else
                        codErr = 0
                    End If
                Else
                    codErr = 0
                End If
            Catch ex As Exception
                codErr = 0
            End Try
        End Function

        Function Lista() As DataTable
            Dim sql As String, objDT As New DataTable
            Dim objGen As New NM_Consulta
            sql = "Select * " & _
            " from NM_Urdimbre where flagestado = 1"
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Function List(ByVal sCodigoMaquina As String) As DataTable
            Dim sql As String, objDT As New DataTable
            Dim objGen As New NM_Consulta
            sql = "Select * " & _
            " from NM_MA_Urdimbre where codigo_maquina='" & sCodigoMaquina & _
            "' order by codigo_urdimbre"
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Function ListaCodigo() As DataTable
            Dim sql As String, objDT As New DataTable
            Dim objGen As New NM_Consulta
            sql = "Select codigo_urdimbre " & _
            " from NM_Urdimbre where flagestado = 1 order by codigo_urdimbre"
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Function listAll() As DataTable
            Dim sql As String, objDT As New DataTable
            Dim objGen As New NM_Consulta
            sql = "Select * " & _
            " from NM_Urdimbre where flagestado = 1 order by codigo_urdimbre"
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Function ListarPorCodigo(ByVal strCodigoUrdimbre As String) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"p_var_CodigoUrdimbre", strCodigoUrdimbre}
                Return _objConexion.ObtenerDataTable("usp_PRO_Urdimbre_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function Obtener(ByVal Codigo As String, ByVal Revision As Integer) As DataTable
            Dim objgen As New NM_Consulta
            Dim sql As String, objDT As New DataTable
            sql = "Select * " & _
            " from NM_Urdimbre where codigo_urdimbre ='" & _
            Codigo & "' and revision_urdimbre = " & Revision
            objDT = objgen.Query(sql)
            Return objDT
        End Function

        Sub deleteDetalle(ByVal txtcodigo_urdimbre As String, ByVal srevision_urdimbre As Integer, ByVal txttipo As String, ByVal txtitem As String)
            Dim objDetalle As New NM_DetalleUrdimbre
            objDetalle.delete(txtcodigo_urdimbre, txttipo, txtitem, srevision_urdimbre)
        End Sub

        Sub LoadTrama()
            Dim objDetalle As New NM_DetalleUrdimbre
            Trama = objDetalle.loadDT(codigo_urdimbre, "Tejido")

        End Sub

        Sub LoadTejido()
            Dim objDetalle As New NM_DetalleUrdimbre
            Tejido = objDetalle.loadDT(codigo_urdimbre, "Tejido")
        End Sub

        Sub loadOrillo()
            Dim objDetalle As New NM_DetalleUrdimbre
            Orillo = objDetalle.loadDT(codigo_urdimbre, "Orillo")
        End Sub

        Public Function insert() As Boolean
            Dim objGen As New NM_Consulta
            Dim objDT As DataTable, sql As String
            Dim objDC As DataColumn
            Dim objDR As DataRow
            Dim objDS As DataSet

            If codigo_urdimbre <> "" Then

                'objDT = New DataTable("NM_Urdimbre")
                'objDC = objDT.Columns.Add("codigo_urdimbre", System.Type.GetType("System.String"))
                'objDC = objDT.Columns.Add("revision_urdimbre", System.Type.GetType("System.Single"))
                'objDC = objDT.Columns.Add("codigo_maquina", System.Type.GetType("System.String"))
                'objDC = objDT.Columns.Add("velocidad", System.Type.GetType("System.Single"))
                'objDC = objDT.Columns.Add("numero_hilos", System.Type.GetType("System.Int32"))
                'objDC = objDT.Columns.Add("posicion_fileta", System.Type.GetType("System.Int32"))
                'objDC = objDT.Columns.Add("peso_metro_cuadrado", System.Type.GetType("System.Single"))
                'objDC = objDT.Columns.Add("tension", System.Type.GetType("System.Int32"))
                'objDC = objDT.Columns.Add("presion_tambor", System.Type.GetType("System.Single"))
                'objDC = objDT.Columns.Add("peso_metro_lineal", System.Type.GetType("System.Single"))
                'objDC = objDT.Columns.Add("usuario_creacion", System.Type.GetType("System.String"))
                'objDC = objDT.Columns.Add("fecha_creacion", System.Type.GetType("System.DateTime"))
                'objDC = objDT.Columns.Add("usuario_modificacion", System.Type.GetType("System.String"))
                'objDC = objDT.Columns.Add("fecha_modificacion", System.Type.GetType("System.DateTime"))
                'objDC = objDT.Columns.Add("flagestado", System.Type.GetType("System.Int32"))


                'objDR = objDT.NewRow()
                revision_urdimbre = getLastRevision(codigo_urdimbre)
                sql = "Insert into NM_Urdimbre(codigo_urdimbre,revision_urdimbre,codigo_maquina," & _
                "peso_metro_cuadrado, peso_metro_lineal, flagestado,usuario_creacion," & _
                " fecha_creacion) values('" & UCase(codigo_urdimbre) & "', " & revision_urdimbre + 1 & _
                ",'" & codigo_maquina & "'," & peso_metro_cuadrado & _
                "," & peso_metro_lineal & "," & flagestado & ",'" & usuario_creacion & "', getdate())"
                Return objGen.Execute(sql)
                'objDR("codigo_urdimbre") = codigo_urdimbre
                'objDR("revision_urdimbre") = revision_urdimbre + 1
                'objDR("codigo_maquina") = codigo_maquina
                'objDR("velocidad") = velocidad
                'objDR("numero_hilos") = numero_hilos
                'objDR("posicion_fileta") = posicion_fileta
                'objDR("peso_metro_cuadrado") = peso_metro_cuadrado
                'objDR("tension") = tension
                'objDR("usuario_creacion") = usuario_creacion
                'objDR("presion_tambor") = presion_tambor
                'objDR("fecha_creacion") = fecha_creacion
                'objDR("peso_metro_lineal") = peso_metro_lineal
                'objDR("usuario_modificacion") = usuario_modificacion
                'objDR("fecha_modificacion") = fecha_modificacion
                'objDR("flagestado") = flagestado

                'objDT.Rows.Add(objDR)
                'objDS = New DataSet()
                'objDS.Tables.Add(objDT)
                'SendHistory(codigo_urdimbre) ' asigna 0 a los estados de los registros anteriores para poder asignarle 1 al nuuevo registro
                'If objGen.Insert(objDS) Then
                'End If

            End If
            Return False
        End Function

        Function Update()
            Dim sql As String, codErr As Integer = 0, objTHilo As NM_THilo
            Dim objTable As New DataTable, objGen As New NM_Consulta
            Try
                If codigo_urdimbre <> "" Then
                    sql = "Update NM_Urdimbre set " & _
                    "codigo_maquina = '" & codigo_maquina & "'," & _
                    "velocidad = " & velocidad & ", " & _
                    "posicion_fileta = " & posicion_fileta & ", " & _
                    "peso_metro_cuadrado = " & peso_metro_cuadrado & ", " & _
                    "tension = " & tension & ", " & _
                    "presion_tambor = " & presion_tambor & "," & _
                    "peso_metro_lineal = " & peso_metro_lineal & _
                    " where codigo_urdimbre = '" & UCase(codigo_urdimbre) & "' and revision_urdimbre = " & revision_urdimbre
                    codErr = objGen.Execute(sql)
                End If

            Catch ex As Exception
                Throw
                codErr = 0
            End Try

        End Function

        Function Update(ByVal scodigo_urdimbre As String, ByVal srevision_urdimbre As String, ByVal estado As Integer)
            Dim sql As String, codErr As Integer = 0, objTHilo As NM_THilo
            Dim objTable As New DataTable, objGen As New NM_Consulta
            Try

                If scodigo_urdimbre <> "" And srevision_urdimbre <> "" Then
                    sql = "Update NM_Urdimbre set " & _
                    "flagestado = " & estado & _
                    " where codigo_urdimbre = '" & UCase(scodigo_urdimbre) & "' and revision_urdimbre ='" & srevision_urdimbre & "'"

                    codErr = objGen.Execute(sql)
                End If
            Catch ex As Exception
                Throw
                codErr = 0
            End Try

        End Function

        Function getLastRevision(ByVal pcodigo_urdimbre As String) As String
            Dim dt As New DataTable, fila As DataRow
            Dim rev As Integer = 0, objConn As New NM_Consulta
            Dim sql As String
            sql = "Select max(revision_urdimbre) as revision_urdimbre from NM_Urdimbre" & _
            " where codigo_urdimbre ='" & pcodigo_urdimbre & "'  and flagestado = 1 "
            dt = objConn.Query(sql)
            For Each fila In dt.Rows
                If IsDBNull(fila("revision_urdimbre")) Then
                    Return "0"
                Else
                    Return fila("revision_urdimbre")
                End If

            Next
            Return rev
        End Function

        Function CoeficienteUrdimbre(ByVal hilosCmTela As Integer) As Double
            Dim dr As DataRow
            Dim THilo As New NM_THilo
            Dim coeficiente As Double
            Dim totalCoeficiente As Double

            ' Hallar el coeficiente de urdimbre de cada hilo
            For Each dr In Tejido.Rows
                THilo.Seek(dr("codigo_hilo"))
                coeficiente = hilosCmTela / Math.Sqrt(1.694 * THilo.NeReal)
                totalCoeficiente += coeficiente
            Next

            ' Hallar el coeficiente de urdimbre promedio
            'If Tejido.Rows.Count > 0 Then
            '    Return Format(totalCoeficiente / Tejido.Rows.Count, "Fixed")
            'End If

            Return Format(totalCoeficiente, "Fixed")

        End Function

        Function CoeficienteTrama(ByVal Pasadasxcm As Integer) As Double
            Dim dr As DataRow
            Dim THilo As New NM_THilo
            Dim coeficiente As Double
            Dim totalCoeficiente As Double
            Try
                ' Hallar el coeficiente de urdimbre de cada hilo
                For Each dr In Trama.Rows
                    THilo.Seek(dr("codigo_hilo"))
                    coeficiente = Pasadasxcm / Math.Sqrt(1.694 * THilo.NeReal)
                    totalCoeficiente += coeficiente
                Next

                ' Hallar el coeficiente de urdimbre promedio
                'If Trama.Rows.Count > 0 Then
                '    Return Format(totalCoeficiente / Trama.Rows.Count, "Fixed")
                'End If

                Return Format(totalCoeficiente, "Fixed")

            Catch
                Return 0
            End Try
        End Function
    End Class

End Namespace