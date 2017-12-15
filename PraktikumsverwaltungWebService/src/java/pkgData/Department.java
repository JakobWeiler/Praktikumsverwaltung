/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pkgData;

import javax.xml.bind.annotation.XmlRootElement;
import org.bson.types.ObjectId;

/**
 *
 * @author schueler
 */
@XmlRootElement
public class Department {
    private ObjectId id;
    private String name;
    private String description;

    public Department() {
    }

    public Department(ObjectId id, String name, String description) {
        this.id = id;
        this.name = name;
        this.description = description;
    }

    public ObjectId getId() {
        return id;
    }

    public void setId(ObjectId id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }
    
    
}
