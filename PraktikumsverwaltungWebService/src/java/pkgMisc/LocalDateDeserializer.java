/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pkgMisc;

import java.lang.reflect.Type;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.time.LocalDate;
 
import com.google.gson.JsonDeserializationContext;
import com.google.gson.JsonDeserializer;
import com.google.gson.JsonElement;
import com.google.gson.JsonParseException;
import java.time.ZoneId;
import java.util.Date;

/**
 *
 * @author schueler
 */
public class LocalDateDeserializer implements JsonDeserializer<LocalDate> {
    private static final SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd");
    public LocalDate deserialize(JsonElement jsonDate, Type typeOfSrc, JsonDeserializationContext context)
    {
       try
       {
           Date d = new Date(jsonDate.getAsJsonPrimitive().getAsLong());
           LocalDate localDate = dateFormat.parse(d.toString()).toInstant().atZone(ZoneId.systemDefault()).toLocalDate();
           return localDate;
       }
       catch (ParseException e)
       {
           e.printStackTrace();
       }
       return null;
    }    
}
