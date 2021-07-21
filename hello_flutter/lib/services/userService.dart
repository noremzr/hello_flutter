import 'dart:async';
import 'dart:convert';

import 'package:hello_flutter/models/UserModel.dart';
import 'package:hello_flutter/models/UserValidatorModel.dart';
import 'package:hello_flutter/services/BaseService.dart';
import 'package:http/http.dart' as http;
import './BaseService.dart';

class UserService {
  final String route = "users";
  final String urlBase = BaseService().urlApi;

  Future<List<UserModel>> getUsers() async {
    http.Response response =
        await http.get(Uri.parse('$urlBase/$route/getUsers'));

    if (response.statusCode == 200) {
      List<dynamic> body = jsonDecode(response.body);

      List<UserModel> userList =
          body.map((dynamic item) => UserModel.fromJson(item)).toList();
      return userList;
    } else {
      throw "Não deu bom";
    }
  }

  Future<UserModel> getUser(String usuario) async {
    final http.Response response =
        await http.get(Uri.parse('$urlBase/$route/GetUser/$usuario'));

    if (response.statusCode == 200) {
      dynamic body = jsonDecode(response.body);

      UserModel user = UserModel.fromJsonUnique(body);
      return user;
    } else {
      return new UserModel(
          usuario: '', senha: '', codDocumento: '', existe: false);
    }
  }

  Future<UserValidatorModel> validateUser(UserModel usuario) async {
    final http.Response response = await http.post(
      Uri.parse('$urlBase/$route/ValidateUser'),
      body: usuario.toJson(),
    );

    if (response.statusCode == 200) {
      dynamic body = jsonDecode(response.body);

      UserValidatorModel user = UserValidatorModel.fromJsonUnique(body);
      return user;
    } else {
      return new UserValidatorModel(
          usuarioNaoExiste: true, senhaDiferente: false);
    }
  }

  Future<String> deleteUser(UserModel user) async {
    // Map<String, String> headers = {"codDocumento": "${user.codDocumento}"};

    final http.Response res = await http
        .get(Uri.parse('$urlBase/$route/DeleteUser/${user.codDocumento}'));
    if (res.statusCode == 200) {
      return "OK";
    } else {
      return "ERROR";
    }
  }

  Future<UserModel> saveUser(UserModel usuario) async {
    http.Response response = await http
        .post(Uri.parse('$urlBase/$route/PostUser'), body: usuario.toJson());
    if (response.statusCode == 201) {
      List<dynamic> body = jsonDecode(response.body);
      List<UserModel> userList =
          body.map((dynamic item) => UserModel.fromJson(item)).toList();
      return userList.first;
    } else {
      return new UserModel(
          usuario: '', senha: '', codDocumento: '', existe: false);
    }
  }
}
