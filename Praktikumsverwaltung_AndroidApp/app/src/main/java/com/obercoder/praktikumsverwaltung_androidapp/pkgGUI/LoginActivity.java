package com.obercoder.praktikumsverwaltung_androidapp.pkgGUI;

import android.content.Intent;
import android.content.SharedPreferences;
import android.support.v7.app.AppCompatActivity;

import android.os.Bundle;
import android.util.Log;
import android.view.Gravity;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.obercoder.praktikumsverwaltung_androidapp.R;
import com.obercoder.praktikumsverwaltung_androidapp.pkgData.Database;
import com.obercoder.praktikumsverwaltung_androidapp.pkgData.Pupil;

/**
 * A login screen that offers login via email/password.
 */
public class LoginActivity extends AppCompatActivity {

    private Button btnLogin;
    private EditText txtUser;
    private EditText txtPasswd;
    private SharedPreferences sp;
    Database db = Database.newInstance();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        // Set up the login form.
        setContentView(R.layout.activity_login);
        btnLogin = (Button) findViewById(R.id.btnLogin);
        txtUser = (EditText) findViewById(R.id.txtUser);
        txtPasswd = (EditText) findViewById(R.id.txtPassword);
        try {
            db.connect();
            db.addPupil(new Pupil("Sasa", "sasa"));
            Log.d("hallihallo","HU>kjcdan.");
        } catch(Exception ex) {
            ex.printStackTrace();
        }


        if (getIntent().getBooleanExtra("EXIT", false)) {
            finish();
        }
    }

    public void onBtnLogin(View view) {
        Toast t = null;
        try {
            if (txtUser.getText().length() != 0 && txtPasswd.getText().length() != 0) {
                if (db.getListPupil().contains(new Pupil(txtUser.getText().toString(), txtPasswd.getText().toString()))) {
                    startActivity(new Intent(LoginActivity.this, MainActivity.class));
                } else {
                    t = Toast.makeText(this, "Wrong username and/or password !", Toast.LENGTH_LONG);
                }
            } else {
                t = Toast.makeText(this, "Input has to be longer than four characters", Toast.LENGTH_SHORT);
            }

            if (t != null) {
                t.setGravity(Gravity.CENTER, 0, 100);
                t.show();
            }
        } catch(Exception ex) {
            ex.printStackTrace();
        }
    }

    public void onBtnSignUp(View view) {
        try {
            startActivity(new Intent(LoginActivity.this, SignUpActivity.class));

        } catch(Exception ex) {
            ex.printStackTrace();
        }
    }
}

