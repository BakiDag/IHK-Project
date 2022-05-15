import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AzubiManagementComponent } from './ausbilderComponents/azubi-management/azubi-management.component';
import { AuthGuardService } from './guards/auth.service';
import { LoginComponent } from './login/login.component';
import { MitarbeiterManagementComponent } from './adminComponents/mitarbeiter-management/mitarbeiter-management.component';
import { RegisterComponent } from './register/register.component';
import { AusbilderDashboardComponent } from './ausbilderComponents/ausbilder-dashboard/ausbilder-dashboard.component';
import { AzubiDashboardComponent } from './azubiComponents/azubi-dashboard/azubi-dashboard.component';
import { WochenberichtListeComponent } from './azubiComponents/wochenbericht-liste/wochenbericht-liste.component';
import { WochenberichtViewComponent } from './azubiComponents/wochenbericht-view/wochenbericht-view.component';
import { AusbilderMessageCenterComponent } from './ausbilderComponents/ausbilder-message-center/ausbilder-message-center.component';
import { AzubiMessageCenterComponent } from './azubiComponents/azubi-message-center/azubi-message-center.component';
import { MitarbeiterEmailPipe } from './adminComponents/mitarbeiter-management/pipes/mitarbeiter-email.pipe';
import { MitarbeiterNachnamePipe } from './adminComponents/mitarbeiter-management/pipes/mitarbeiter-nachname.pipe';
import { MitarbeiterRolePipe } from './adminComponents/mitarbeiter-management/pipes/mitarbeiter-role.pipe';
import { MitarbeiterVornamePipe } from './adminComponents/mitarbeiter-management/pipes/mitarbeiter-vorname.pipe';
import { AzubiEmailPipe } from './ausbilderComponents/azubi-management/pipes/azubi-email.pipe';
import { AzubiNachnamePipe } from './ausbilderComponents/azubi-management/pipes/azubi-nachname.pipe';
import { AzubiVornamePipe } from './ausbilderComponents/azubi-management/pipes/azubi-vorname.pipe';
import { AdminRegisterComponent } from './adminComponents/admin-register/admin-register.component';
import { ReportControlComponent } from './ausbilderComponents/report-control/report-control.component';



const routes: Routes = [
    //{path: "/app", component:AppComponent},
    {path: "register", component:RegisterComponent},
    {path:"login", component:LoginComponent},
    {path:"azubi-management", component: AzubiManagementComponent, canActivate:[AuthGuardService]},    
    {path:"mitarbeiter-management", component: MitarbeiterManagementComponent, canActivate:[AuthGuardService]},      
    {path:"ausbilder-dashboard", component: AusbilderDashboardComponent, canActivate:[AuthGuardService]},    
    {path:"azubi-dashboard", component: AzubiDashboardComponent, canActivate:[AuthGuardService]},    
    {path:"wochenbericht-liste", component: WochenberichtListeComponent, canActivate:[AuthGuardService]}, 
    {path:"wochenbericht-view", component: WochenberichtViewComponent, canActivate:[AuthGuardService]}, 
    {path:"AusbilderMessageCenter", component: AusbilderMessageCenterComponent, canActivate:[AuthGuardService]}, 
    {path:"AzubiMessageCenter", component: AzubiMessageCenterComponent, canActivate:[AuthGuardService]}, 
    {path:"admin-register", component: AdminRegisterComponent, canActivate:[AuthGuardService]}, 
    {path:"report-control", component: ReportControlComponent,canActivate:[AuthGuardService] },

    //{path: "/app", component:AppComponent},
//     {path: "register", component:RegisterComponent},
//     {path:"login", component:LoginComponent},
//     {path:"azubi-management", component: AzubiManagementComponent },
//     {path:"mitarbeiter-management", component: MitarbeiterManagementComponent},
//     {path:"ausbilder-dashboard", component: AusbilderDashboardComponent},
//     {path:"azubi-dashboard", component: AzubiDashboardComponent},
//     {path:"wochenbericht-liste", component: WochenberichtListeComponent},
//     {path:"wochenbericht-view", component: WochenberichtViewComponent},
//     {path:"AusbilderMessageCenter", component: AusbilderMessageCenterComponent},
//     {path:"AzubiMessageCenter", component: AzubiMessageCenterComponent}, 
//     {path:"admin-register", component: AdminRegisterComponent },
//     {path:"report-control", component: ReportControlComponent },
 ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

export const routingComponents = 
[
  LoginComponent,
  RegisterComponent,
  AzubiManagementComponent,
  MitarbeiterManagementComponent,          
  AusbilderDashboardComponent,
  AzubiDashboardComponent,
  WochenberichtListeComponent,
  WochenberichtViewComponent,
  MitarbeiterNachnamePipe,
  MitarbeiterEmailPipe,
  MitarbeiterNachnamePipe,
  MitarbeiterRolePipe,
  MitarbeiterVornamePipe,    
  AzubiVornamePipe,
  AzubiNachnamePipe,
  AzubiEmailPipe,    
  AusbilderMessageCenterComponent, 
  AzubiMessageCenterComponent,
  AdminRegisterComponent

]
