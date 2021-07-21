import 'package:hello_flutter/models/UserModel.dart';

import '../shared/convertions.dart';

class AutorModel {
  String? nome;
  String? observacao;
  int? codAutor;
  String? codDocumento;
  bool? existe;

  AutorModel({
    required this.nome,
    required this.observacao,
    required this.codAutor,
    required this.codDocumento,
    required this.existe,
  });

  AutorModel.n(this.nome, this.observacao);

  factory AutorModel.fromJson(Map<String, dynamic> json) {
    return AutorModel(
      nome: json['nome'] as String?,
      observacao: json['observacao'] as String?,
      codAutor: json['codAutor'] as int?,
      codDocumento: json['codDocumento'] as String?,
      existe: Convertions.stringToBoolean(json['existe'].toString()) as bool?,
    );
  }

  factory AutorModel.fromJsonUnique(dynamic json) {
    return AutorModel(
      nome: json['nome'] as String?,
      observacao: json['observacao'] as String?,
      codAutor: json['codAutor'] as int?,
      codDocumento: json['codDocumento'] as String?,
      existe: Convertions.stringToBoolean(json['existe'].toString()) as bool?,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'nome': nome,
      'observacao': observacao,
      'codAutor': codAutor,
      'codDocumento': codDocumento,
      'existe': "$existe",
    };
  }

  String getDescriptionDropdown(AutorModel autor) {
    return "${autor.nome} (${autor.codAutor})";
  }
}

class AutorFilter {
  String? nome;
  int? codAutor;

  Map<String, dynamic> toJson() {
    return {
      'nome': nome,
      'codAutor': codAutor,
    };
  }
}
