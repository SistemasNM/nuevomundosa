Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos
Imports System.Text
Imports System.Xml
Imports System.IO

Namespace NM_Hilanderia

    Public Class NM_DatosProduccionD

        Public codigo_produccion As String
        Public codigo_maquina As String
        Public revision_maquina As Integer
        Public Ne_nominal As Double
        Public husos_maquina As Integer
        Public puntaje As Double
        Public peso As Double
        Public velocidad As Double
        Public usuario As String
        Private DB As NM_Consulta
        Private objUtil As New NM_General.Util

        Sub New()
            codigo_produccion = ""
            codigo_maquina = ""
            revision_maquina = 0
            Ne_nominal = 0
            husos_maquina = 0
            puntaje = 0
            peso = 0
        End Sub

        Public Function Add() As Boolean
            If codigo_produccion <> "" AndAlso codigo_maquina <> "" AndAlso Ne_nominal <> 0 Then
                DB = New NM_Consulta(4)
                Dim strsql As String
                strsql = "INSERT INTO NM_DatosProduccionD(codigo_produccion," & _
                "codigo_maquina, revision_maquina, ne_nominal, " & _
                " husos_maquina, puntaje, peso, velocidad_real, usuario_creacion, " & _
                "fecha_creacion) values(" & _
                "'" & codigo_produccion & "','" & codigo_maquina & "'," & _
                revision_maquina & "," & Ne_nominal & "," & _
                husos_maquina & "," & puntaje & "," & peso & ", " & _
                velocidad & ", '" & usuario & "',GetDate())"
                Return DB.Execute(strsql)
            Else
                Return False
            End If
        End Function

        Public Function update() As Boolean
            If codigo_produccion <> "" AndAlso codigo_maquina <> "" AndAlso Ne_nominal <> 0 Then
                DB = New NM_Consulta(4)
                Dim strsql As String
                strsql = "UPDATE NM_DatosProduccionD SET " & _
                "husos_maquina = " & husos_maquina & "," & _
                "puntaje = " & puntaje & "," & _
                "velocidad_real = " & velocidad & "," & _
                "peso = " & peso & "," & _
                "usuario_modificacion = '" & usuario & "'," & _
                "fecha_modificacion = getdate() " & _
                " where codigo_produccion = '" & codigo_produccion & "'" & _
                " and codigo_maquina = '" & codigo_maquina & "'" & _
                " and Ne_nominal = " & Ne_nominal
                Return DB.Execute(strsql)
            Else
                Return False
            End If
        End Function

        Public Function delete(ByVal pcodigo_produccion As String, ByVal pcodigo_maquina As String, ByVal pNe_nominal As Double) As Boolean
            If pcodigo_produccion <> "" AndAlso pcodigo_maquina <> "" AndAlso pNe_nominal <> 0 Then
                Dim strsql As String
                DB = New NM_Consulta(4)
                strsql = "DELETE FROM NM_DatosProduccionD where codigo_produccion = '" & pcodigo_produccion & "'"
                strsql = strsql & " and codigo_maquina = '" & pcodigo_maquina & "'"
                strsql = strsql & " and Ne_nominal = '" & pNe_nominal & "'"
                Return DB.Execute(strsql)
            Else
                Return False
            End If
        End Function

        Public Function list(ByVal pcodigo_produccion As String) As DataTable
            DB = New NM_Consulta(4)
            Return DB.Query("SELECT * FROM NM_DatosProduccionD " & _
            "WHERE codigo_produccion = '" & pcodigo_produccion & "' order by fecha_creacion desc")
        End Function

        Public Function list(ByVal dFecha As String, ByVal nTurno As Integer) As DataTable
            DB = New NM_Consulta(4)
            Dim sql As String
            sql = "SELECT D.* " & _
            "FROM NM_DatosProduccion P, NM_DatosProduccionD D " & _
            " WHERE P.codigo_produccion = D.codigo_produccion " & _
            " and fecha = '" & objUtil.FormatFechaHora(dFecha) & "' and turno=" & nTurno
            Return DB.Query(sql)
        End Function

        Function ValidaMaquina(ByVal pMaquina As String, ByVal pTitulo As String, ByVal pRevision As String) As String
            Dim t As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
            Dim objParametros As Object() = {"CodigoMaquina", pMaquina, "Revision", pRevision, "Titulo", pTitulo}
            ValidaMaquina = Convert.ToString(t.ObtenerValor("NM_SEL_TITULO_MAQ", objParametros))

        End Function

        Public Sub grabarDatosProduccion(ByVal dtDatosProduccion As DataTable, _
                ByVal CodigoProduccion As String, ByVal fecha As String, ByVal turno As Integer, _
                ByVal usuario As String)

            Try
                Dim t As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim objParametros As Object() = {"xml_data", GeneraXml(dtDatosProduccion), _
                        "Codigo_Produccion", CodigoProduccion, "Fecha", fecha, _
                        "Turno", turno, "Usuario_Creacion", usuario}

                t.EjecutarComando("MN_SAV_DATOS_PRODU_HILA_V2", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

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

        Public Function cargar(ByVal dFecha As Date, ByVal nturno As Integer) As DataSet

            Dim t As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
            Dim objParametros As Object() = {"FechaIn", dFecha, "TurnoIn", nturno}
            cargar = t.ObtenerDataSet("NM_SEL_DATO_PRODUCCION", objParametros)

        End Function

        ''' <summary>
        ''' LUIS_AJ (20211012)
        ''' </summary>
        ''' <param name="dFecha"></param>
        ''' <param name="nturno"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ObtenerCodigoHilo(ByVal codigo_maquina As String, ByVal revision_maquina As String, ByVal titulo As String) As String

            Dim ObjHilanderia As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
            Dim strResultado As String

            Try
                Dim objParametros As Object() = {"pvch_CodigoMaquina", codigo_maquina,
                                                 "pint_RevisionMaquina", revision_maquina,
                                                 "pdec_Titulo", titulo}

                ObjHilanderia.ObtenerValor("USP_HIL_OBTENER_CODIGOHILO_PRODUCCION", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                ObjHilanderia = Nothing
            End Try

            Return strResultado

        End Function

        ''' <summary>
        ''' LUIS_AJ (20211012)
        ''' </summary>
        ''' <param name="dFecha"></param>
        ''' <param name="nturno"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ObtenerCodigoFamilia(ByVal codigo_hilo As String) As String

            Dim ObjHilanderia As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
            Dim strResultado As String

            Try
                Dim objParametros As Object() = {"pvch_CodigoHilo", codigo_hilo}

                ObjHilanderia.ObtenerValor("USP_HIL_OBTENER_CODIGOFAMILIA_PRODUCCION", objParametros)
            Catch ex As Exception
                Throw ex
            Finally
                ObjHilanderia = Nothing
            End Try

            Return strResultado

        End Function

        ''' <summary>
        ''' GTAIRA
        ''' </summary>
        ''' <param name="dFecha"></param>
        ''' <param name="nturno"></param>
        ''' <remarks></remarks>
        Public Sub ELIMINAR_DATOS_PRODUCCION(ByVal dFecha As Date, ByVal nturno As Integer)
            Try
                Dim t As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim objParametros As Object() = {"FechaIn", dFecha, "TurnoIn", nturno}

                t.EjecutarComando("NM_ELIMINAR_DATO_PRODUCCION", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

    End Class

End Namespace

