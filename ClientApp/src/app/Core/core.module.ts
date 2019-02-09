import { NgModule, ModuleWithProviders, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { ServicesModule } from './services/services.module';

@NgModule({
    imports: [
        CommonModule,
        RouterModule,
        HttpClientModule,
        ReactiveFormsModule,
        ServicesModule.forRoot()
    ],
    declarations: [
    ],
    exports: [
        // Modules
        RouterModule,
        ReactiveFormsModule,
        ServicesModule
    ]
})

export class CoreModule {
    // https://angular.io/guide/singleton-services#prevent-reimport-of-the-coremodule
    constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
        if (parentModule) {
            throw new Error(
                'CoreModule is already loaded. Import it in the AppModule only');
        }
    }

    static forRoot(): ModuleWithProviders {
        return {
            ngModule: CoreModule,
            providers: []
        };
    }
}
