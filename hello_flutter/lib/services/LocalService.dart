import 'dart:convert';

import 'package:shared_preferences/shared_preferences.dart';

class LocalService {
  static String userVar = "user";
  static Future<void> setLocalItem(String key, String item) async {
    final prefs = await SharedPreferences.getInstance();
    prefs.setString(key, item);
  }

  static Future<Map<String, dynamic>> getLocalItem(String key) async {
    final prefs = await SharedPreferences.getInstance();
    return await jsonDecode(prefs.getString(key)!) as Map<String, dynamic>;
  }

  static Future<void> removeItem(String key) async {
    final prefs = await SharedPreferences.getInstance();
    prefs.remove(key);
  }
}
