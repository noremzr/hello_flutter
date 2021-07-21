import '../shared/convertions.dart';

class UserModel {
  String? usuario;
  String? senha;
  String? codDocumento;
  bool? existe;

  UserModel({
    required this.usuario,
    required this.senha,
    required this.codDocumento,
    required this.existe,
  });

  factory UserModel.fromJson(Map<String, dynamic> json) {
    return UserModel(
      usuario: json['usuario'] as String?,
      senha: json['senha'] as String?,
      codDocumento: json['codDocumento'] as String?,
      existe: Convertions.stringToBoolean(json['existe'].toString()) as bool?,
    );
  }

  factory UserModel.fromJsonUnique(dynamic json) {
    return UserModel(
      usuario: json['usuario'] as String?,
      senha: json['senha'] as String?,
      codDocumento: json['codDocumento'] as String?,
      existe: Convertions.stringToBoolean(json['existe'].toString()) as bool?,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'usuario': usuario,
      'senha': senha,
      'codDocumento': codDocumento,
      'existe': "$existe",
    };
  }
}
