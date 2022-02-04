Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Hilanderia

  Public Class NM_Maquina

    Private m_objConnection As AccesoDatosSQLServer

    Public Usuario As String
    Public codigo_maquina As String
    Public Nombre As String
    Public revision_maquina As Integer
    Public codigo_linea As String
    Public codigo_tipo_maquina As String
    Public Ne_nominal As Double
    Public Ne_real As Double
    Public numero_husos As Integer
    Public velocidad As Double
    Public kilos_hora As Double
    Public FlagEstado As Integer

    Sub New()
      m_objConnection = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)

      codigo_maquina = ""
      codigo_linea = ""
      FlagEstado = 0
    End Sub

    Sub New(ByVal codigoMaquina As String)
      m_objConnection = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)

      Seek(codigoMaquina)
    End Sub

    Function Add() As Boolean
      Dim bd As New NM_Consulta(4)

      If codigo_maquina <> "" Then
        Dim sql = "INSERT INTO NM_Maquina " & _
            "(codigo_maquina, codigo_linea, " & _
            "codigo_tipo_maquina, revision_maquina, " & _
            "Ne_nominal, Ne_real, " & _
            "numero_husos, velocidad, kilos_hora, " & _
            "usuario_creacion, fecha_creacion, flagestado) " & _
            "VALUES ('" & _
            codigo_maquina & "', '" & _
            codigo_linea & "', '" & _
            codigo_tipo_maquina & "', " & _
            revision_maquina & ", " & _
            Replace(Ne_nominal, ",", ".") & ", " & _
            Replace(Ne_real, ",", ".") & ", " & _
            numero_husos & ", " & _
            Replace(velocidad, ",", ".") & ", " & _
            Replace(kilos_hora, ",", ".") & ", '" & _
            Usuario & "'," & _
            "GetDate(), " & FlagEstado & ")"
        Return bd.Execute(sql)
      Else
        Throw New Exception("No se puede insertar porque el código es incorrecto.")
      End If
    End Function

    Function Update() As Boolean
      Dim bd As New NM_Consulta(4)

      If codigo_maquina <> "" Then
        Dim sql = "UPDATE NM_Maquina " & _
            "SET codigo_linea = '" & codigo_linea & "', " & _
            "flagestado=" & FlagEstado & "," & _
            "codigo_tipo_maquina = '" & codigo_tipo_maquina & "', " & _
            "Ne_nominal = " & Replace(Ne_nominal, ",", ".") & ", " & _
            "Ne_real = " & Replace(Ne_real, ",", ".") & ", " & _
            "numero_husos = " & numero_husos & ", " & _
            "kilos_hora = " & Replace(kilos_hora, ",", ".") & ", " & _
            "velocidad = " & Replace(velocidad, ",", ".") & ", " & _
            "usuario_modificacion = '" & Usuario & "', " & _
            "fecha_modificacion = GetDate() " & _
            "WHERE codigo_maquina = '" & codigo_maquina & "' " & _
            " and revision_maquina = " & revision_maquina & " "
        Return bd.Execute(sql)
      Else
        Throw New Exception("No se puede actualizar porque el código es inválido.")
      End If
    End Function

    Public Sub Seek(ByVal codigoMaquina As String)
      Dim bd As New NM_Consulta(4)
      Dim sql As String, objMaq As New NM_Produccion.NM_Tejeduria.NM_Maquina
      Dim objDT As New DataTable
      Dim objDR As DataRow
      sql = "SELECT * from NM_Maquina where codigo_maquina = '" & codigoMaquina & "' and flagestado = 1"
      objDT = bd.Query(sql)
      For Each objDR In objDT.Rows
        If Not IsDBNull(objDR("codigo_maquina")) Then codigo_maquina = objDR("codigo_maquina")
        If Not IsDBNull(objDR("codigo_linea")) Then codigo_linea = objDR("codigo_linea")
        If Not IsDBNull(objDR("codigo_tipo_maquina")) Then codigo_tipo_maquina = objDR("codigo_tipo_maquina")
        If Not IsDBNull(objDR("revision_maquina")) Then revision_maquina = objDR("revision_maquina")
        If Not IsDBNull(objDR("Ne_nominal")) Then Ne_nominal = objDR("Ne_nominal")
        If Not IsDBNull(objDR("Ne_real")) Then Ne_real = objDR("Ne_real")
        If Not IsDBNull(objDR("numero_husos")) Then numero_husos = objDR("numero_husos")
        If Not IsDBNull(objDR("velocidad")) Then velocidad = objDR("velocidad")
        If Not IsDBNull(objDR("kilos_hora")) Then kilos_hora = objDR("kilos_hora")
        objMaq.seek(codigo_maquina)
        Nombre = objMaq.nombre_corto
      Next
      objMaq = Nothing
    End Sub

    Function Exist(ByVal codigoMaquina As String) As Boolean
      Dim objGen As New NM_Consulta(4)
      Dim sql As String
      Dim objDT As New DataTable
      sql = "Select * from NM_Maquina where codigo_maquina = '" & codigoMaquina & "'"
      objDT = objGen.Query(sql)
      If objDT.Rows.Count > 0 Then
        Return True
      Else
        Return False
      End If
    End Function

    Function Delete(ByVal sCodigoMaquina As String) As Boolean
      Dim objGen As New NM_Consulta(4)
      Dim sql As String
      Try
        sql = "Delete from NM_Maquina where codigo_maquina = '" & sCodigoMaquina & "'"
        Return objGen.Execute(sql)
      Catch
        Return False
      End Try
    End Function

    Function List() As DataTable
      Dim bd As New NM_Consulta(4)

      'Dim sql = "SELECT * FROM NM_Maquina WHERE flagestado = 1"
      Dim sql = "SELECT l.descripcion_linea, tm.descripcion_tipo_maquina, m.* " & _
          "FROM NM_Maquina m " & _
          "JOIN NM_Linea l " & _
          "ON m.codigo_linea = l.codigo_linea " & _
          "JOIN NM_TipoMaquina tm " & _
          "ON m.codigo_tipo_maquina = tm.codigo_tipo_maquina " & _
          "WHERE flagestado = 1"
      Return bd.Query(sql)
    End Function

    Function ListD() As DataTable
      Dim bd As New NM_Consulta(4)
      Dim dt As DataTable
      Dim dr As DataRow

      Dim sql = "SELECT codigo_maquina, revision_maquina FROM NM_Maquina " & _
          "WHERE flagestado = 1"
      dt = bd.Query(sql)
      dt.Columns.Add("descripcion_maquina")

      Dim maquinaTeje As New NM_Produccion.NM_Tejeduria.NM_Maquina

      For Each dr In dt.Rows
        maquinaTeje.seek(dr("codigo_maquina"))
        dr("descripcion_maquina") = maquinaTeje.nombre_corto
      Next

      Return dt
    End Function

    Function ListD(ByVal codigoLinea As String, ByVal codigoTipoMaquina As String) As DataTable
      Dim bd As New NM_Consulta(4)
      Dim dt As DataTable
      Dim dr As DataRow

      Dim sql = "SELECT codigo_maquina, revision_maquina FROM NM_Maquina " & _
          "WHERE codigo_linea = '" & codigoLinea & "' " & _
          "AND codigo_tipo_maquina = '" & codigoTipoMaquina & "' " & _
          "AND flagestado = 1"
      dt = bd.Query(sql)
      dt.Columns.Add("descripcion_maquina")

      Dim maquinaTeje As New NM_Produccion.NM_Tejeduria.NM_Maquina

      For Each dr In dt.Rows
        maquinaTeje.seek(dr("codigo_maquina"))
        dr("descripcion_maquina") = maquinaTeje.nombre_corto
      Next

      Return dt
    End Function
    Function Lista_Maquina(ByVal strCodigoMaquina As String, ByVal strDescriMaquina As String, ByRef dtData As DataTable) As Boolean
      'Dim bd As New NM_Consulta(4)
      'Dim dt As DataTable
      'Dim dr As DataRow

      'Dim sql = "SELECT codigo_maquina, revision_maquina FROM NM_Maquina " & _
      '    "WHERE codigo_linea = '" & codigoLinea & "' " & _
      '    "AND codigo_tipo_maquina = '" & codigoTipoMaquina & "' " & _
      '    "AND flagestado = 1"
      'dt = bd.Query(sql)
      'dt.Columns.Add("descripcion_maquina")

      'Dim maquinaTeje As New NM_Produccion.NM_Tejeduria.NM_Maquina

      'For Each dr In dt.Rows
      '    maquinaTeje.seek(dr("codigo_maquina"))
      '    dr("descripcion_maquina") = maquinaTeje.nombre_corto
      'Next

      'Return dt
      Dim bResultado As Boolean = False
      Try
        Dim objParametros() As Object = {"vch_CodigoMaquina", strCodigoMaquina, _
                                         "vch_DescripMaquina", strDescriMaquina}
        dtData = m_objConnection.ObtenerDataTable("USP_HIL_MAQUINA_LISTAR", objParametros)
        bResultado = True
      Catch ex As Exception
        Throw ex
      End Try
      Return bResultado
    End Function
    Function SegmentoMaquina_Agregar(ByVal strCodigoMaquina As String, ByVal strSegmento As String, ByVal strCodigoHilo As String, ByVal strUsuario As String, ByRef dtData As DataTable) As Boolean
      Dim bResultado As Boolean = False
      Try
        Dim objParametros() As Object = {"vch_CodigoMaquina", strCodigoMaquina, "vch_RutaSegmento", strSegmento, "vch_CodigoHilo", strCodigoHilo, "vch_Usuario", strUsuario}
        m_objConnection.EjecutarComando("USP_HIL_PARAMETROSMAQUINA_INSERTAR", objParametros)
        bResultado = True
      Catch ex As Exception
        Throw ex
      End Try
      Return bResultado
    End Function
    Function SegmentoMaquina_Listar(ByVal strCodigoMaquina As String, ByVal strSegmento As String, ByVal strCodigoHilo As String, ByRef dtData As DataTable) As Boolean
      Dim bResultado As Boolean = False
      Try
        Dim objParametros() As Object = {"vch_CodigoMaquina", strCodigoMaquina,
                                          "vch_Rutasegmento", strSegmento,
                                         "vch_CodigoHilo", strCodigoHilo}
        dtData = m_objConnection.ObtenerDataTable("USP_HIL_SEGEMENTOMAQUINA_LISTAR", objParametros)
        bResultado = True
      Catch ex As Exception
        Throw ex
      End Try
      Return bResultado
    End Function
    Function SegmentoMaquinaSelec_Listar(ByVal strRutaSegmento As String, ByVal strDescripcion As String, ByVal strCodigoMaquina As String, ByRef dtData As DataTable) As Boolean
      Dim bResultado As Boolean = False
      Try
        Dim objParametros() As Object = {"vch_Rutasegmento", strRutaSegmento,
                                         "vch_Descripcion", strDescripcion,
                                         "vch_CodigoMaquina", strCodigoMaquina}
        dtData = m_objConnection.ObtenerDataTable("USP_HIL_SEGMENTOMAQUINASELEC_LISTAR", objParametros)
        bResultado = True
      Catch ex As Exception
        Throw ex
      End Try
      Return bResultado
    End Function
    Public Function ParametrosSegmentoMaquina_Listar(ByVal strMaquina As String, ByVal strSegmento As String, ByVal strtitulo As String, ByRef dtData As DataTable) As Boolean
      Dim bResultado As Boolean = False
      Try
        Dim objParametros As Object() = {"vch_CodigoMaquina", strMaquina,
                                         "vch_codigoSegmento", strSegmento,
                                         "vch_CodigoHilo", strtitulo}
        dtData = m_objConnection.ObtenerDataTable("USP_HIL_PARAMETROSEGEMENTOMAQUINA_LISTAR", objParametros)
        bResultado = True
      Catch ex As Exception
        bResultado = False
        Throw ex
      End Try
      Return bResultado
        End Function
        Public Function ParametrosHiloMaquina_Listar(ByVal strMaquina As String, ByRef dtData As DataTable) As Boolean
            Dim bResultado As Boolean = False
            Try
                Dim objParametros As Object() = {"vch_CodigoMaquina", strMaquina}
                dtData = m_objConnection.ObtenerDataTable("USP_HIL_RutaParametroHilaMaquina_Listar", objParametros)
                bResultado = True
            Catch ex As Exception
                bResultado = False
                Throw ex
            End Try
            Return bResultado
        End Function
    Public Function SegmentoMaquina_Selec(ByVal strMaquina As String, ByRef dtData As DataTable) As Boolean
      Dim bResultado As Boolean = False
      Try
        Dim objParametros As Object() = {"vch_CodigoMaquina", strMaquina}
        dtData = m_objConnection.ObtenerDataTable("USP_HIL_MaquinaSegmento_Listar", objParametros)
        bResultado = True
      Catch ex As Exception
        bResultado = False
        Throw ex
      End Try
      Return bResultado
    End Function
    Public Function MaquinaParametroDetalle_Selec(ByVal strMaquina As String, ByVal strSegmento As String, ByRef dtData As DataTable) As Boolean
      Dim bResultado As Boolean = False
      Try
        Dim objParametros As Object() = {"vch_CodigoMaquina", strMaquina,
                                         "vch_RutaSegmento", strSegmento}
        dtData = m_objConnection.ObtenerDataTable("USP_HIL_MaquinaParametroDetalle_Listar", objParametros)
        bResultado = True
      Catch ex As Exception
        bResultado = False
        Throw ex
      End Try
      Return bResultado
    End Function
    Public Function SegmentoMaquina_Actualizar(ByVal strMaquina As String, ByVal strUsuario As String, ByVal dtData As DataTable) As Boolean
      Dim bResultado As Boolean = False
      Dim lobjUtil = New NM_General.Util
      Dim lblValor As Boolean = False
      Try
        dtData.TableName = "Data"
        Dim objParametros As Object() = {"vch_CodigoMaquina", strMaquina, _
                                         "vch_Usuario", strUsuario, _
                                         "pnvc_detalle", lobjUtil.GeneraXml(dtData)}

        m_objConnection.EjecutarComando("usp_NM_MaquinaParametro_Insertar", objParametros)
        bResultado = True
      Catch ex As Exception
        bResultado = False
        Throw ex
      End Try
      Return bResultado
    End Function
    Public Function MaquinaParametroHilo_Eliminar(ByVal strMaquina As String, ByVal strSegmento As String, ByVal strCodigoHilo As String) As Boolean
      Dim bResultado As Boolean = False
      Try
        Dim objParametros As Object() = {"vch_CodigoMaquina", strMaquina, _
                                         "vch_RutaSegmento", strSegmento, _
                                         "vch_CodigoHilo", strCodigoHilo}

        m_objConnection.EjecutarComando("USP_HIL_HILOPARAMETROS_ELIMINAR", objParametros)
        bResultado = True
      Catch ex As Exception
        bResultado = False
        Throw ex
      End Try
      Return bResultado
    End Function
    Public Function MaquinaParametroDetalle_Actualizar(ByVal strMaquina As String, ByVal strSegmento As String, ByVal strUsuario As String, ByVal dtData As DataTable) As Boolean
      Dim bResultado As Boolean = False
      Dim lobjUtil = New NM_General.Util
      Dim lblValor As Boolean = False
      Try
        dtData.TableName = "Data"
        Dim objParametros As Object() = {"vch_CodigoMaquina", strMaquina, _
                                         "vch_RutaSegmento", strSegmento, _
                                         "vch_Usuario", strUsuario, _
                                         "pnvc_detalle", lobjUtil.GeneraXml(dtData)}

        m_objConnection.EjecutarComando("usp_NM_MaquinaParametroDetalle_Insertar", objParametros)
        bResultado = True
      Catch ex As Exception
        bResultado = False
        Throw ex
      End Try
      Return bResultado
    End Function

    Public Function ParametrosSegmentoMaquina_Actualizar(ByVal strMaquina As String, _
                                            ByVal strSegmento As String, _
                                            ByVal strTitulo As String, _
                                            ByVal strUsuario As String, _
                                           ByVal dtbData As DataTable) As Boolean
      Dim lobjUtil As NM_General.Util
      Dim lblValor As Boolean = False
      Dim strCadena As String = ""

      If Not dtbData Is Nothing And dtbData.Rows.Count > 0 Then
        lobjUtil = New NM_General.Util
        strCadena = lobjUtil.GeneraXml(dtbData)
        If Trim(strCadena.Length) > 0 Then
          Try
            dtbData.TableName = "Data"
            Dim objParametros() As Object = {"vch_CodigoMaquina", strMaquina, _
                                             "vch_RutaSegmento", strSegmento, _
                                             "vch_Codigohilo", strTitulo, _
                                            "vch_Usuario", strUsuario, _
                                            "pnvc_detalle", strCadena}
            m_objConnection.EjecutarComando("USP_HIL_PARAMETROSSEGMENTOMAQUINA_ACTUALIZAR", objParametros)
            lblValor = True
          Catch ex As Exception
            lblValor = False
            Throw ex
          Finally
            lobjUtil = Nothing
          End Try
        End If
      End If
      Return lblValor
    End Function

    Function List(ByVal codigoLinea As String, ByVal codigoTipoMaquina As String) As DataTable
      Dim bd As New NM_Consulta(4)

      Dim sql = "SELECT * FROM NM_Maquina " & _
          "WHERE codigo_linea = '" & codigoLinea & "' " & _
          "AND codigo_tipo_maquina = '" & codigoTipoMaquina & "' " & _
          "AND flagestado = 1"
      Return bd.Query(sql)
    End Function

    Function List(ByVal codigoLinea As String) As DataTable
      Dim bd As New NM_Consulta(4)

      Dim sql = "SELECT * FROM NM_Maquina " & _
          "WHERE codigo_linea = '" & codigoLinea & "' " & _
          "AND flagestado = 1"
      Return bd.Query(sql)
    End Function

    Function Reserva(ByVal sCodigoMaquina As String)
      Dim objparametros() As Object = {"codigo_maquina", sCodigoMaquina}

      m_objConnection.EjecutarComando("NM_UPD_RESERVARDATOSMAQUINA", objparametros)

    End Function

    Public Sub CambiarRevision()
      Dim objGen As New NM_Consulta(4)
      Dim sql As String
      Dim objDT As New DataTable
      Dim objDR As DataRow

      sql = " UPDATE NM_Maquina SET "
      sql += " flagestado=0 "
      sql += " Where codigo_maquina='" & codigo_maquina & "'"
      sql += " and flagestado = 1 "
      objGen.Execute(sql)

    End Sub

    Function KgHora(ByVal codTipoMaquina As String, ByVal ptitulo As Double) As Double
      Dim bd As New NM_Consulta(4)
      Dim dt As DataTable
      Dim fila As DataRow

      Dim sql = "SELECT AVG(kilos_hora) FROM NM_Maquina WHERE codigo_tipo_maquina = '" & codTipoMaquina & "' and ne_nominal = " & ptitulo
      dt = bd.Query(sql)

      For Each fila In dt.Rows
        If Not IsDBNull(fila(0)) Then
          Return fila(0)
        Else
          Return 0
        End If
      Next
    End Function

    Public Function GetRevision(ByVal sCodigoMaquina As String)
      Dim bd As New NM_Consulta(4)
      Dim dt As New DataTable, fila As DataRow
      Dim nRev As Integer = 0
      Dim sql = "SELECT revision_maquina FROM NM_Maquina " & _
          "WHERE codigo_maquina = '" & sCodigoMaquina & "' " & _
          "AND flagestado = 1"
      dt = bd.Query(sql)
      For Each fila In dt.Rows
        nRev = fila("revision_maquina")
      Next
      Return nRev
        End Function

        ''' <summary>
        ''' OBTENER ULTIMA REVISION
        ''' </summary>
        ''' <param name="sCodigoMaquina"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetUltimaRevision(ByVal sCodigoMaquina As String)
            Dim bd As New NM_Consulta(4)
            Dim dt As New DataTable, fila As DataRow
            Dim nRev As Integer = 0

            Dim sql = "SELECT max(revision_maquina) as revision_maquina FROM NM_Maquina " & _
                      "WHERE codigo_maquina = '" & sCodigoMaquina & "'"
            dt = bd.Query(sql)
            For Each fila In dt.Rows
                nRev = fila("revision_maquina")
            Next
            Return nRev
        End Function

    Function ListNMProd() As DataTable
      Dim bd As New NM_Consulta(1)

      Dim sql = "SELECT codigo_maquina As CODIGO, nombre_corto As NOMBRE FROM NM_Maquina "
      Return bd.Query(sql)
    End Function

    Function Descripcion(ByVal pMaquina As String) As String
      Dim dtMaquina As DataTable
      Dim bd As New NM_Consulta(1)
      Dim desc As String

      Dim sql = "SELECT codigo_maquina As CODIGO, nombre_corto As NOMBRE FROM NM_Maquina where codigo_maquina='" & pMaquina & "'"
      dtMaquina = bd.Query(sql)
      If dtMaquina.Rows.Count > 0 Then
        desc = dtMaquina.Rows(0)("NOMBRE")
      Else
        desc = String.Empty
      End If

      Return desc
    End Function


        Function BUSCAR_DATOS_MAQUINA_HILANDERIA(ByVal strCodigoMaquina As String, ByVal strCodUsuario As String, ByVal strOpcion As String) As DataTable

            Dim objparametros() As Object = {"vch_CodMaquina", strCodigoMaquina,
                                             "vch_CodUsuario", strCodUsuario,
                                             "vch_Opcion", strOpcion}

            Return m_objConnection.ObtenerDataTable("USP_BUSCAR_DATOS_MAQUINA_HILANDERIA", objparametros)

        End Function
  End Class

End Namespace
