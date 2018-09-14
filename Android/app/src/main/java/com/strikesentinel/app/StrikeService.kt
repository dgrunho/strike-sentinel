package com.strikesentinel.app

/**
 * Created by Tan on 6/26/2015.
 */


import retrofit.Callback
import retrofit.http.Body
import retrofit.http.DELETE
import retrofit.http.GET
import retrofit.http.POST
import retrofit.http.PUT
import retrofit.http.Path


interface InstituteService {
    //Retrofit turns our institute WEB API into a Java interface.
    //So these are the list available in our WEB API and the methods look straight forward

    //i.e. http://localhost/api/institute/Students
    @GET("/StrikeNews/Dummy")
    fun getStrike(callback: Callback<List<Strike>>)

    @GET("/StrikeNews/GroupDummy")
    fun getStrikeGroup(callback: Callback<List<StrikeGroup>>)

    //i.e. http://localhost/api/institute/Students/1
    //Get student record base on ID
    @GET("/StrikeNews/{id}")
    fun getStudentById(@Path("id") id: Int?, callback: Callback<Strike>)

    //i.e. http://localhost/api/institute/Students/1
    //Delete student record base on ID
    @DELETE("/StrikeNews/{id}")
    fun deleteStudentById(@Path("id") id: Int?, callback: Callback<Strike>)

    //i.e. http://localhost/api/institute/Students/1
    //PUT student record and post content in HTTP request BODY
    @PUT("/StrikeNews/{id}")
    fun updateStudentById(@Path("id") id: Int?, @Body student: Strike, callback: Callback<Strike>)

    //i.e. http://localhost/api/institute/Students
    //Add student record and post content in HTTP request BODY
    @POST("/StrikeNews")
    fun addStudent(@Body student: Strike, callback: Callback<Strike>)

}