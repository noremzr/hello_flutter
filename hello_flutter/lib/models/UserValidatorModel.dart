class UserValidatorModel {
  bool? usuarioNaoExiste;
  bool? senhaDiferente;

  UserValidatorModel({
    required this.usuarioNaoExiste,
    required this.senhaDiferente,
  });

  factory UserValidatorModel.fromJson(Map<String, dynamic> json) {
    return UserValidatorModel(
      usuarioNaoExiste: json['usuarioNaoExiste'] as bool?,
      senhaDiferente: json['senhaDiferente'] as bool?,
    );
  }

  factory UserValidatorModel.fromJsonUnique(dynamic json) {
    return UserValidatorModel(
      senhaDiferente: json['senhaDiferente'] as bool?,
      usuarioNaoExiste: json['usuarioNaoExiste'] as bool?,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'senhaDiferente': senhaDiferente,
      'usuarioNaoExiste': usuarioNaoExiste,
    };
  }
}
