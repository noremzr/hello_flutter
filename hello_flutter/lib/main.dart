import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:hello_flutter/widgets/Menu.dart';
import 'package:hello_flutter/widgets/autores/autores.dart';
import 'package:hello_flutter/widgets/login.dart';

void main() => runApp(MyApp());

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Usuarios Demo',
      theme: ThemeData(
        primarySwatch: Colors.grey,
      ),
      debugShowCheckedModeBanner: false,
      home: Login(),
      routes: {
        '/Menu': (_) => new MenuFrm(),
        '/Login': (_) => new Login(),
        '/Autores': (_) => new MyWidget(),
      },
    );
  }
}
