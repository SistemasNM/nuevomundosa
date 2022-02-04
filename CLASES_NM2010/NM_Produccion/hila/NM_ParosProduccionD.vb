Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos
Imports System.Text
Imports System.Xml
Imports System.IO

Namespace NM_Hilanderia

    Public Class NM_ParosProduccionD

        Public Usuario As String
        Public codigo_linea As String
        Public codigo_tipo_maquina As String
        Public codigo_paro_produccionD As String
        Public descripcion_paro_produccion As String
        Public Ne_nominal As Double
        Public tarifa_hora As Double
        Public porcentaje_tarifa As Double

        Function Add()
            Dim bd As New NM_Consulta(4)

            If codigo_linea <> "" And codigo_tipo_maquina <> "" And codigo_paro_produccionD <> "" Then
                Dim sql = "INSERT INTO NM_ParosProduccionD " & _
                    "(codigo_linea, codigo_tipo_maquina, codigo_paro_produccionD, " & _
                    "descripcion_paro_produccion, Ne_nominal, " & _
                    "tarifa_hora, porcentaje_tarifa, " & _
                    "usuario_creacion, fecha_creacion) " & _
                    "VALUES ('" & _
                    codigo_linea & "', '" & _
                    codigo_tipo_maquina & "', '" & _
                    codigo_paro_produccionD & "', '" & _
                    descripcion_paro_produccion & "', " & _
                    Ne_nominal & ", " & _
                    tarifa_hora & ", " & _
                    porcentaje_tarifa & ", '" & _
                    Usuario & "'," & _
                    "GetDate())"
                Return bd.Execute(sql)
            Else
                Throw New Exception("No se puede insertar porque el código es incorrecto.")
            End If
        End Function

        Function Update()
            Dim bd As New NM_Consulta(4)

            If codigo_linea <> "" And codigo_tipo_maquina <> "" And codigo_paro_produccionD <> "" Then
                Dim sql = "UPDATE NM_ParosProduccionD " & _
                    "SET descripcion_paro_produccion = '" & descripcion_paro_produccion & "', " & _
                    "tarifa_hora = " & tarifa_hora & ", " & _
                    "porcentaje_tarifa = " & porcentaje_tarifa & ", " & _
                    "usuario_modificacion = '" & Usuario & "', " & _
                    "fecha_modificacion = GetDate() " & _
                    "WHERE codigo_linea = '" & codigo_linea & "' " & _
                    "AND codigo_tipo_maquina = '" & codigo_tipo_maquina & "' " & _
                    "AND codigo_paro_produccionD = '" & codigo_paro_produccionD & "'" & _
                    " and Ne_nominal = " & Ne_nominal & " "
                Return bd.Execute(sql)
            Else
                Throw New Exception("No se puede actualizar porque el código es inválido.")
            End If
        End Function

        Function Delete(ByVal codigoLinea As String, ByVal codigoTipoMaquina As String, ByVal codigoParoD As String)
            Dim bd As New NM_Consulta(4)

            If codigoLinea <> "" And codigoTipoMaquina <> "" And codigoParoD <> "" Then
                Dim sql = "DELETE FROM NM_ParosProduccionD " & _
                    "WHERE codigo_linea = '" & codigoLinea & "' " & _
                    "AND codigo_tipo_maquina = '" & codigoTipoMaquina & "' " & _
                    "AND codigo_paro_produccionD = '" & codigoParoD & "'"
                Return bd.Execute(sql)
            Else
                Throw New Exception("No se puede eliminar porque el código no es válido.")
            End If
        End Function

        ''' <summary>
        ''' GTAIRA (20211220)
        ''' </summary>
        ''' <param name="codigoLinea"></param>
        ''' <param name="codigoTipoMaquina"></param>
        ''' <param name="codigoParoD"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function DeleteParo(ByVal codigoLinea As String, ByVal codigoTipoMaquina As String, ByVal codigoParoD As String)
            Dim bd As New NM_Consulta(4)

            If codigoLinea <> "" And codigoTipoMaquina <> "" And codigoParoD <> "" Then
                Dim sql = "DELETE FROM NM_MaquinaParo " & _
                    "WHERE codigo_linea = '" & codigoLinea & "' " & _
                    "AND codigo_tipo_maquina = '" & codigoTipoMaquina & "' " & _
                    "AND codigo_maquina_paro = '" & codigoParoD & "'"
                Return bd.Execute(sql)
            Else
                Throw New Exception("No se puede eliminar porque el código no es válido.")
            End If
        End Function

        Function Delete(ByVal codigoLinea As String, ByVal codigoTipoMaquina As String, _
        ByVal codigoParoD As String, ByVal nTitulo As Double)
            Dim bd As New NM_Consulta(4)

            If codigoLinea <> "" And codigoTipoMaquina <> "" And codigoParoD <> "" Then
                Dim sql = "DELETE FROM NM_ParosProduccionD " & _
                    "WHERE codigo_linea = '" & codigoLinea & "' " & _
                    "AND codigo_tipo_maquina = '" & codigoTipoMaquina & "' " & _
                    "AND codigo_paro_produccionD = '" & codigoParoD & "' " & _
                    " and NE_Nominal=" & nTitulo
                Return bd.Execute(sql)
            Else
                Throw New Exception("No se puede eliminar porque el código no es válido.")
            End If
        End Function

        Function List(ByVal codigoPrograma As String) As DataTable
            Dim bd As New NM_Consulta(4)

            Dim sql = "SELECT * FROM NM_ParosProduccionD WHERE codigo_programa = '" & codigoPrograma & "'"
            Return bd.Query(sql)
        End Function

        Function List(ByVal codigoLinea As String, ByVal codigoTipoMaquina As String) As DataTable
            Dim bd As New NM_Consulta(4)
            Dim sql = "SELECT * FROM NM_ParosProduccionD " & _
                    "WHERE codigo_linea = '" & codigoLinea & "' " & _
                    "AND codigo_tipo_maquina = '" & codigoTipoMaquina & "' "
            Return bd.Query(sql)
        End Function

        Public Sub Seek(ByVal codigoLinea As String, ByVal codigoTipoMaquina As String, ByVal pCodigo_paro_produccionD As String)
            Dim bd As New NM_Consulta(4)
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            sql = "SELECT * FROM NM_ParosProduccionD WHERE codigo_linea = '" & codigoLinea & "' " & _
                    "AND codigo_tipo_maquina = '" & codigoTipoMaquina & "' and Codigo_paro_produccionD = '" & pCodigo_paro_produccionD & "'"
            objDT = bd.Query(sql)
            For Each objDR In objDT.Rows
                If Not IsDBNull(objDR("codigo_linea")) Then codigo_linea = objDR("codigo_linea")
                If Not IsDBNull(objDR("codigo_tipo_maquina")) Then codigo_linea = objDR("codigo_tipo_maquina")
                If Not IsDBNull(objDR("codigo_paro_produccionD")) Then codigo_paro_produccionD = objDR("codigo_paro_produccionD")
                If Not IsDBNull(objDR("descripcion_paro_produccion")) Then descripcion_paro_produccion = objDR("descripcion_paro_produccion")
                If Not IsDBNull(objDR("Ne_nominal")) Then Ne_nominal = objDR("Ne_nominal")
                If Not IsDBNull(objDR("tarifa_hora")) Then tarifa_hora = objDR("tarifa_hora")
                If Not IsDBNull(objDR("porcentaje_tarifa")) Then porcentaje_tarifa = objDR("porcentaje_tarifa")
            Next
        End Sub

        Public Function buscarTiposParo(ByVal codigoMaquina As String, _
            ByVal fechaInicio As String, ByVal turno As Integer) As DataSet
            'Dim objUtil As New NM_Produccion.NM_Util.NM_Util
            Dim objUtil As New NM_General.Util

            Dim t As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
            Dim objParametros As Object() = {"CodigoMaquina", codigoMaquina, _
                    "FechaInicio", objUtil.FormatFecha(objUtil.convierteFecha(fechaInicio)), "Turno", turno}
            buscarTiposParo = t.ObtenerDataSet("NM_SEL_MAQUINA_PARO", objParametros)

        End Function

        Public Sub grabarParos(ByVal dtParosMaquina As DataTable)

            Dim t As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
            Dim objParametros As Object() = {"xml_data", GeneraXml(dtParosMaquina)}
            t.EjecutarComando("MN_SAV_PARO_PRODU_HILA", objParametros)

        End Sub

        Function GeneraXml(ByVal dtDatos As DataTable) As String
            Dim xmlDOM As New XmlDocument
            Dim nodo, nodoChild As XmlElement
            Dim objXML As New NM_General.Util
            With xmlDOM
                .Load(New StringReader("<root></root>"))
                For i As Integer = 0 To dtDatos.Rows.Count - 1
                    nodo = .CreateElement(dtDatos.TableName)
                    For j As Integer = 0 To dtDatos.Columns.Count - 1
                        'If Not IsDBNull(dtDatos.Rows(i)(j)) Then
                        nodoChild = .CreateElement(dtDatos.Columns(j).ColumnName)
                        nodoChild.InnerText = Convert.ToString(dtDatos.Rows(i)(j))
                        nodo.AppendChild(nodoChild)
                        'End If
                    Next j
                    .DocumentElement.AppendChild(nodo)
                Next i

                Return objXML.EncodeXML(.OuterXml())
            End With
        End Function
    End Class

End Namespace
