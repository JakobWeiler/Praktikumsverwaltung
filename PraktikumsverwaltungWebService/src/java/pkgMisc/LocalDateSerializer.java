/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pkgMisc;

import java.lang.reflect.Type;
import java.text.SimpleDateFormat;
import java.time.LocalDate;
 
import com.google.gson.JsonElement;
import com.google.gson.JsonPrimitive;
import com.google.gson.JsonSerializationContext;
import com.google.gson.JsonSerializer;

/**
 *
 * @author schueler
 */
public class LocalDateSerializer implements JsonSerializer<LocalDate> {
    private static final SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd");
    public JsonElement serialize(LocalDate date, Type typeOfSrc, JsonSerializationContext context)
    {
       return new JsonPrimitive(dateFormat.format(date));
    }
}
