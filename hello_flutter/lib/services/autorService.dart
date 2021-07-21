import 'dart:async';
import 'dart:convert';

import 'package:hello_flutter/models/AutorModel.dart';
import 'package:hello_flutter/services/BaseService.dart';
import 'package:http/http.dart' as http;
import './BaseService.dart';

class AutorService {
  final String route = "autores";
  final String urlBase = BaseService().urlApi;

  Future<List<AutorModel>> getAutores(AutorFilter? autorFilter) async {
    http.Response response =
        await http.get(Uri.parse('$urlBase/$route/GetAutores'));

    if (response.statusCode == 200) {
      List<dynamic> body = jsonDecode(response.body);

      List<AutorModel> autoresList =
          body.map((dynamic item) => AutorModel.fromJson(item)).toList();
      return autoresList;
    } else {
      throw "Não deu bom";
    }
  }

  Future<List<AutorModel>> getAutorByName(String nome) async {
    final http.Response response =
        await http.get(Uri.parse('$urlBase/$route/GetAutorByName/$nome'));

    if (response.statusCode == 200) {
      List<dynamic> body = jsonDecode(response.body);

      List<AutorModel> autoresList =
          body.map((dynamic item) => AutorModel.fromJson(item)).toList();
      return autoresList;
    } else {
      throw "Não deu bom";
    }
  }

  Future<AutorModel> getAutorByCod(int codAutor) async {
    final http.Response response =
        await http.get(Uri.parse('$urlBase/$route/GetAutorByCod/$codAutor'));

    if (response.statusCode == 200) {
      dynamic body = jsonDecode(response.body);

      AutorModel autor = AutorModel.fromJsonUnique(body);
      return autor;
    } else {
      return new AutorModel(
          nome: '',
          observacao: '',
          codAutor: 0,
          codDocumento: '',
          existe: false);
    }
  }

  Future<String> deleteAutor(AutorModel autor) async {
    final http.Response res = await http
        .get(Uri.parse('$urlBase/$route/DeleteAutor/${autor.codDocumento}'));
    if (res.statusCode == 200) {
      return "OK";
    } else {
      return "ERROR";
    }
  }

  Future<AutorModel> saveAutor(AutorModel autor) async {
    http.Response response = await http
        .post(Uri.parse('$urlBase/$route/PostAutor'), body: autor.toJson());
    if (response.statusCode == 201) {
      List<dynamic> body = jsonDecode(response.body);
      List<AutorModel> autorList =
          body.map((dynamic item) => AutorModel.fromJson(item)).toList();
      return autorList.first;
    } else {
      return new AutorModel(
          nome: '',
          observacao: '',
          codAutor: 0,
          codDocumento: '',
          existe: false);
    }
  }
}
