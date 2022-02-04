Imports NM.AccesoDatos

Public Class ProgramarMaquina


  Public Function Listar_NroProg(ByVal sCodigo_Maquina As String) As DataTable
    Dim lobjTinto As AccesoDatosSQLServer
    Dim mdsSet As DataTable
    Dim lbooOk As Boolean

    Try
      lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
      Dim larrParametros() As String = {"Codigo_Maquina", sCodigo_Maquina}

      Return (CType(lobjTinto.ObtenerDataTable("usp_ListaProgramacion", larrParametros), DataTable))
    Catch ex As Exception
      Throw ex
    Finally
      lobjTinto = Nothing
      mdsSet = Nothing
    End Try
  End Function

  Public Function Listar_ParosMaquinaTinto(ByVal sFecIni As String, ByVal sFecFin As String) As Integer
    Dim lobjTinto As AccesoDatosSQLServer
    Dim mdsSet As DataTable
    Dim lbooOk As Boolean

    Try
      lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
      Dim larrParametros() As String = {"FechaIni", sFecIni, _
                                        "FechaFin", sFecFin, _
                                        "codigo_maquina", ""}

      Return lobjTinto.EjecutarComando("Usp_NM_ListaParoProdTinto", larrParametros)
    Catch ex As Exception
      Throw ex
    Finally
      lobjTinto = Nothing
      mdsSet = Nothing
    End Try
  End Function



  Public Function Listar_UltimaProg(ByVal sCodigo_Maquina As String, ByVal iNumero_Programa As Integer) As DataTable
    Dim lobjTinto As AccesoDatosSQLServer
    Dim mdsSet As DataTable
    Dim lbooOk As Boolean

    Try
      lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
      Dim larrParametros() As String = {"Codigo_Maquina", sCodigo_Maquina, "Numero_Programa", iNumero_Programa}

      Return (CType(lobjTinto.ObtenerDataTable("usp_UltimaProgramacion", larrParametros), DataTable))
    Catch ex As Exception
      Throw ex
    Finally
      lobjTinto = Nothing
      mdsSet = Nothing
    End Try
  End Function


  Public Function Listar_Ficha(ByVal sCodigo_Ficha_Partida As String) As DataTable
    Dim lobjTinto As AccesoDatosSQLServer
    Dim mdsSet As DataTable
    Dim lbooOk As Boolean

    Try
      lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
      Dim larrParametros() As String = {"codigo_ficha_partida", sCodigo_Ficha_Partida}

      Return (CType(lobjTinto.ObtenerDataTable("usp_DatosFicha_Sel", larrParametros), DataTable))
    Catch ex As Exception
      Throw ex
    Finally
      lobjTinto = Nothing
      mdsSet = Nothing
    End Try
  End Function

  Public Function Listar_Programacion(ByVal sCodigo_Maquina As String, ByVal iNumero_Programa As Integer) As DataTable
    Dim lobjTinto As AccesoDatosSQLServer
    Dim mdsSet As DataTable
    Dim lbooOk As Boolean

    Try
      lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
      Dim larrParametros() As String = {"Codigo_Maquina", sCodigo_Maquina, _
                                        "Numero_Programa", iNumero_Programa}

      Return (CType(lobjTinto.ObtenerDataTable("usp_NM_Programar_Maquina_Sel", larrParametros), DataTable))
    Catch ex As Exception
      Throw ex
    Finally
      lobjTinto = Nothing
      mdsSet = Nothing
    End Try
  End Function

  Public Function Actualizar_Programacion(ByVal Codigo_Maquina As String, _
                                          ByVal iNumero_Programa As Integer, _
                                          ByVal iSecuencia_Prog As Integer, _
                                          ByVal Codigo_Ficha As String, _
                                          ByVal Nro_Metros As Decimal, _
                                          ByVal Metros_Minuto As Decimal, _
                                          ByVal Hora_Prepara As String, _
                                          ByVal Hora_PrepIni As String, _
                                          ByVal Hora_PrepFin As String, _
                                          ByVal Hora_TeniIni As String, _
                                          ByVal Hora_TeniFin As String, _
                                          ByVal Observacion As String, _
                                          ByVal Usuario_Creacion As String, _
                                          ByVal Accion As String) As String


    Dim strCadena As String
    Dim lobjTinto As AccesoDatosSQLServer
    Dim parametros As Object() = {"Codigo_Maquina", Codigo_Maquina, _
                                  "Numero_Programa", iNumero_Programa, _
                                  "Secuencia_Prog", iSecuencia_Prog, _
                                  "Codigo_Ficha_Partida", Codigo_Ficha, _
                                  "Nro_Metros", Nro_Metros, _
                                  "Metros_Minuto", Metros_Minuto, _
                                  "Hora_Prepara", Hora_Prepara, _
                                  "Hora_PrepIni", Hora_PrepIni, _
                                  "Hora_PrepFin", Hora_PrepFin, _
                                  "Hora_TeniIni", Hora_TeniIni, _
                                  "Hora_TeniFin", Hora_TeniFin, _
                                  "Observacion", Observacion, _
                                  "Usuario_Creacion", Usuario_Creacion, _
                                  "Accion", Accion}

    Try

      lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
      strCadena = CType(lobjTinto.ObtenerValor("usp_NM_Programar_Maquina_Act", parametros), String)

      If strCadena <> "" Then
        Return strCadena
      Else
        Return ""
      End If

    Catch ex As Exception
      strCadena = ex.Message
    End Try

  End Function

  Public Function Eliminar_Programacion(ByVal Codigo_Maquina As String, _
                                        ByVal iNumero_Programa As Integer, _
                                        ByVal iSecuencia_Prog As Integer) As String


    Dim strCadena As String
    Dim lobjTinto As AccesoDatosSQLServer
    Dim parametros As Object() = {"Codigo_Maquina", Codigo_Maquina, _
                                  "Numero_Programa", iNumero_Programa, _
                                  "Secuencia_Prog", iSecuencia_Prog}

    Try

      lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
      strCadena = CType(lobjTinto.ObtenerValor("usp_NM_Programar_Maquina_Del", parametros), String)

      If strCadena <> "" Then
        Return strCadena
      Else
        Return ""
      End If

    Catch ex As Exception
      strCadena = ex.Message
    End Try

  End Function


  Public Function Reprogramar_Paros(ByVal pMaquina As String, _
                                    ByVal pProgra As Integer, _
                                    ByVal pNumSec As Integer, _
                                    ByVal pHoraParar As String, _
                                    ByVal pObservacion As String, _
                                    ByVal pUsuario As String) As String


    Dim strCadena As String
    Dim lobjTinto As AccesoDatosSQLServer
    Dim parametros As Object() = {"pMaquina", pMaquina, _
                                  "pNroPrograma", pProgra, _
                                  "pNumSec", pNumSec, _
                                  "pHoraParar", pHoraParar, _
                                  "pObservacion", pObservacion, _
                                  "pUsuario", pUsuario}

    Try

      lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
      strCadena = CType(lobjTinto.ObtenerValor("usp_CalcularProgramacion", parametros), String)

      If strCadena <> "" Then
        Return strCadena
      Else
        Return ""
      End If

    Catch ex As Exception
      strCadena = ex.Message
    End Try

  End Function


End Class
