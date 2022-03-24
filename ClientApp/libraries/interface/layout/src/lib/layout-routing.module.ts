import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { MainContentComponent } from './components/main-content/main-content.component';

const routes: Routes = [
    {
        path: '', component: MainContentComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class LayoutRoutingModule {
}
