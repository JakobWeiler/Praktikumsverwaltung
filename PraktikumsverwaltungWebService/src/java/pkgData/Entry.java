/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pkgData;

import java.time.LocalDate;
import javax.xml.bind.annotation.XmlRootElement;
import org.bson.types.ObjectId;

/**
 *
 * @author schueler
 */
@XmlRootElement
public class Entry {
    private String id;
    private LocalDate startDate;
    private LocalDate endDate;
    private double salary;
    private String title;
    private String description;
    private boolean allowedTeacher;
    private boolean allowedAV;
    private boolean seenByAdmin;
    private String idPupil;
    private String idCompany;
    private String idClass;

    public Entry() {
    }

    public Entry(String id, LocalDate startDate, LocalDate endDate, double salary, String title, String description, boolean allowedTeacher, boolean allowedAV, boolean seenByAdmin, String idPupil, String idCompany, String idClass) {
        this.id = id;
        this.startDate = startDate;
        this.endDate = endDate;
        this.salary = salary;
        this.title = title;
        this.description = description;
        this.allowedTeacher = allowedTeacher;
        this.allowedAV = allowedAV;
        this.seenByAdmin = seenByAdmin;
        this.idPupil = idPupil;
        this.idCompany = idCompany;
        this.idClass = idClass;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public LocalDate getStartDate() {
        return startDate;
    }

    public void setStartDate(LocalDate startDate) {
        this.startDate = startDate;
    }

    public LocalDate getEndDate() {
        return endDate;
    }

    public void setEndDate(LocalDate endDate) {
        this.endDate = endDate;
    }

    public double getSalary() {
        return salary;
    }

    public void setSalary(double salary) {
        this.salary = salary;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public boolean isAllowedTeacher() {
        return allowedTeacher;
    }

    public void setAllowedTeacher(boolean allowedTeacher) {
        this.allowedTeacher = allowedTeacher;
    }

    public boolean isAllowedAV() {
        return allowedAV;
    }

    public void setAllowedAV(boolean allowedAV) {
        this.allowedAV = allowedAV;
    }

    public boolean isSeenByAdmin() {
        return seenByAdmin;
    }

    public void setSeenByAdmin(boolean seenByAdmin) {
        this.seenByAdmin = seenByAdmin;
    }

    public String getIdPupil() {
        return idPupil;
    }

    public void setIdPupil(String idPupil) {
        this.idPupil = idPupil;
    }

    public String getIdCompany() {
        return idCompany;
    }

    public void setIdCompany(String idCompany) {
        this.idCompany = idCompany;
    }

    public String getIdClass() {
        return idClass;
    }

    public void setIdClass(String idClass) {
        this.idClass = idClass;
    }
    
    
}
