/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pkgServices;

import java.util.Set;
import javax.ws.rs.core.Application;

/**
 *
 * @author schueler
 */
@javax.ws.rs.ApplicationPath("resources")
public class ApplicationConfig extends Application {

    @Override
    public Set<Class<?>> getClasses() {
        Set<Class<?>> resources = new java.util.HashSet<>();
        addRestResourceClasses(resources);
        return resources;
    }

    /**
     * Do not modify addRestResourceClasses() method.
     * It is automatically populated with
     * all resources defined in the project.
     * If required, comment out calling this method in getClasses().
     */
    private void addRestResourceClasses(Set<Class<?>> resources) {
        resources.add(pkgServices.ClassResource.class);
        resources.add(pkgServices.CompanyResource.class);
        resources.add(pkgServices.DepartmentResource.class);
        resources.add(pkgServices.EntryAdminResource.class);
        resources.add(pkgServices.EntryAndroidResource.class);
        resources.add(pkgServices.EntryResource.class);
        resources.add(pkgServices.LoginResource.class);
        resources.add(pkgServices.PupilResource.class);
        resources.add(pkgServices.TeacherResource.class);
    }
    
}
