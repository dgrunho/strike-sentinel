package com.strikesentinel.app

import org.json.JSONObject

class Strike(strikeJSON: JSONObject) {
    var id: String? = null
    var tipo: String? = null
    var dataInicio: String? = null
    var dataFim: String? = null
    var observacoes: String? = null
    var todoDia: String? = null
    var estado: String? = null
    var estadoDescr: String? = null
    var cor: String? = null
    var empresa: String? = null
    var sourceLink: String? = null
    var dateGroup: String? = null

    init {
        id = strikeJSON.getString("id")
        tipo = strikeJSON.getString("tipo")
        dataInicio = strikeJSON.getString("dataInicio")
        dataFim = strikeJSON.getString("dataFim")
        observacoes = strikeJSON.getString("observacoes")
        todoDia = strikeJSON.getString("todoDia")
        estado = strikeJSON.getString("estado")
        estadoDescr = strikeJSON.getString("estadoDescr")
        cor = strikeJSON.getString("cor")
        empresa = strikeJSON.getString("empresa")
        sourceLink = strikeJSON.getString("sourceLink")
        dateGroup = strikeJSON.getString("dateGroup")
    }

}