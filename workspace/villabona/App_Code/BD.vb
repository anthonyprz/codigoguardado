Imports Microsoft.VisualBasic
Imports System.Data.SqlClient

Public Class BD

    Shared Function Update(ByVal queryString As String) As Integer
        Dim dbConnection = current.SqlConnection
        Dim dbCommand As SqlCommand = New SqlCommand(queryString, dbConnection)
        Dim rowsAffected As Integer = 0
        dbConnection.Open()
        Try
            rowsAffected = dbCommand.ExecuteNonQuery
            Return rowsAffected
        Catch
            Return -1
        Finally
            dbConnection.Close()
        End Try
    End Function

    Shared Function Insert(ByVal queryString As String) As Integer
        Dim dbConnection = current.SqlConnection
        Dim dbCommand As SqlCommand = New SqlCommand(queryString, dbConnection)
        Dim rowsAffected As Integer = 0
        dbConnection.Open()
        Try
            rowsAffected = dbCommand.ExecuteNonQuery
            Return rowsAffected
        Catch
            Return -1
        Finally
            dbConnection.Close()
        End Try
    End Function

    Shared Function InsertID(ByVal queryString As String) As Integer
        Dim dbConnection = current.SqlConnection
        queryString += "; SELECT SCOPE_IDENTITY()"
        Dim dbCommand As SqlCommand = New SqlCommand(queryString, dbConnection)
        Dim rowsAffected As Integer = 0
        dbConnection.Open()
        Try
            rowsAffected = CInt(dbCommand.ExecuteScalar())
            Return rowsAffected
        Catch
            Return -1
        Finally
            dbConnection.Close()
        End Try
    End Function

    Shared Function Eliminar(ByVal queryString As String) As Integer
        Dim dbConnection = current.SqlConnection
        Dim dbCommand As SqlCommand = New SqlCommand(queryString, dbConnection)
        Dim rowsAffected As Integer = 0
        dbConnection.Open()
        Try
            rowsAffected = dbCommand.ExecuteNonQuery
        Finally
            dbConnection.Close()
        End Try
        Return rowsAffected
    End Function

    Shared Function GetDatatable(ByVal queryString As String) As System.Data.DataTable
        Dim dbConnection = current.SqlConnection
        Try
            Dim dbCommand As SqlCommand = New SqlCommand(queryString, dbConnection)
            Dim dataAdapter As SqlDataAdapter = New SqlDataAdapter
            dataAdapter.SelectCommand = dbCommand
            Dim dataSet As System.Data.DataSet = New System.Data.DataSet
            dataAdapter.Fill(dataSet)
            Return dataSet.Tables(0)
        Catch
            Return Nothing
        End Try
    End Function

    Shared Function GetDataset(ByVal queryString As String) As System.Data.DataSet
        Dim dbConnection = current.SqlConnection
        Dim dbCommand As SqlCommand = New SqlCommand(queryString, dbConnection)

        Dim dataAdapter As SqlDataAdapter = New SqlDataAdapter
        dataAdapter.SelectCommand = dbCommand
        Dim dataSet As System.Data.DataSet = New System.Data.DataSet
        dataAdapter.Fill(dataSet)
        Return dataSet
    End Function

    Shared Function GetReader(ByVal queryString As String, ByRef dbConnection As SqlConnection) As SqlDataReader
        Dim dbCommand As SqlCommand = New SqlCommand(queryString, dbConnection)
        dbConnection.Open()
        Try
            Return dbCommand.ExecuteReader
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Shared Function GetBoolean(ByVal queryString As String) As Boolean
        Dim dbConnection = current.SqlConnection
        Dim dbCommand As SqlCommand = New SqlCommand(queryString, dbConnection)
        dbConnection.Open()
        Try
            Return dbCommand.ExecuteScalar()
        Catch
            Return 0
        Finally
            dbConnection.Close()
        End Try
    End Function

    Shared Function GetInteger(ByVal queryString As String) As Integer
        Dim dbConnection = current.SqlConnection
        Dim dbCommand As SqlCommand = New SqlCommand(queryString, dbConnection)
        dbConnection.Open()
        Try
            Return dbCommand.ExecuteScalar()
        Catch
            Return 0
        Finally
            dbConnection.Close()
        End Try
    End Function

    Shared Function GetNumeric(ByVal queryString As String) As Double
        Dim dbConnection = current.SqlConnection
        Dim dbCommand As SqlCommand = New SqlCommand(queryString, dbConnection)
        dbConnection.Open()
        Try
            Return dbCommand.ExecuteScalar()
        Catch
            Return 0
        Finally
            dbConnection.Close()
        End Try
    End Function

    Shared Function GetString(ByVal queryString As String) As String
        Dim dbConnection = current.SqlConnection
        Dim dbCommand As SqlCommand = New SqlCommand(queryString, dbConnection)
        dbConnection.Open()
        Try
            Return dbCommand.ExecuteScalar()
        Catch
            Return "NULL"
        Finally
            dbConnection.Close()
        End Try
    End Function

    Shared Function GetDouble(ByVal queryString As String) As Double
        Dim dbConnection = current.SqlConnection
        Dim dbCommand As SqlCommand = New SqlCommand(queryString, dbConnection)
        dbConnection.Open()
        Try
            Return dbCommand.ExecuteScalar()
        Catch
            Return 0
        Finally
            dbConnection.Close()
        End Try
    End Function

    Shared Function Del_Server(ByVal path As String) As Boolean
        Try
            'Dim rutapath As String = HttpContext.Current.Request.PhysicalApplicationPath & path.Replace("~/", "")
            If System.IO.File.Exists(path) Then
                System.IO.File.Delete(path)
            End If
            Return True
        Catch
            Return False
        End Try
    End Function
End Class
