Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsEsquemas

    Public Enum enu_esquemas
        DistConsumoEExGTrabajadores = 1 'grupo consumo de EE de trabajadores
        DistConsumoEExGBombaAgua = 2 'grupo consumo de EE de bombas de agua
        DistHorasxGMaqPretejido = 3 'grupo de maquinas de pre-tejido
        GuardarHorasxGHorasEstandar = 4 'grupo de horas estandar
        DistConsumoGas = 5
        GuardarDatosVelocidad = 6
        DistConsumoAgua = 7
        DistConsumoEEPlantaPrincipal = 8
        DistConsumoEEPlantaHilanderia = 9
        GuardarOrdenCalculoAgua = 10
        GuardarOrdenCalculoGas = 11
        GuardarOrdenCalculoEEPPR = 12
        GuardarOperacionesUConsumo = 13
        GuardarDetalleUConsumo = 14
        DistCalculoEExGLuwa = 15 'grupo de cálculo de EE de luwas
        DistCalculoEExGIluminacion = 16 'grupo de cálculo de EE de iluminación
        DistConsumoEExGLuwa = 17 'grupo de distribución de EE de luwas
        DistConsumoEExGIluminacion = 18 'grupo de distribución de EE de iluminación
        GuardarHorasxGHorasMaquinaHil = 19 'grupo de horas máquina de hilandería
        DistConsumoGLP = 20 'cálculo de consumo de GLP
        DistConsumoVapor = 21 'cálculo de consumo de vapor
        DistConsumoAirePlantaPrincipal = 22 'cálculo de consumo de aire PPR
        DistConsumoAirePlantaHilanderia = 23 'cálculo de consumo de aire PHI
        GuardarCCProductivoxCCSoporte = 24 'matriz con % de CCproduc x CCsoporte
        GuardarEstampadoCorduroy = 25
        RecuInvValorizado = 26 '%de avance para el inv. valorizado de la recubridora
        GuardarTransfCardaRingOE = 27 'transferencia de cardas de linea Anillos a OE
        GuardarAvanceInvRing = 28 'avance de inventario de linea anillos
        GuardarAvanceInvOE = 29 'avance de inventario de oe
        PFCAlgodonNoValido = 30 'PFC MP algodon no validos
        PFCMerma = 31 ' PFC Resumen de merma 
        PFCCostoAlgodon = 32 'PFC Costo de Algodon
        PFCCostoReposicion = 33 'PFC Costo de Reposicion de Algodón
        PFCCostoPonderadoFV = 34 'PFC Costo Ponderado por hilo de los 4 ultimos meses
        MaqVelocidadFamilia = 35 'Maquina Velocidad x familia
    End Enum

    Public Function ObtenerEsquemas(ByRef pdsDataset As DataSet, ByVal penu_esquema As enu_esquemas) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      19-11-2009
        'Proposito :      Devuelve los esquemas necesarios donde se va a guardar los datos en xml
        '*******************************************************************************************
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pint_esquema", penu_esquema}

        Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
        pdsDataset = Conexion.ObtenerDataSet("usp_cos_esquematablas_obtener", objParametro)

        Try

            blnRpta = True
        Catch ex As Exception
            blnRpta = False
        Finally

        End Try

        Return blnRpta

    End Function

End Class
