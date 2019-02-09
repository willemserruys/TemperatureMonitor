import { NgModule, ModuleWithProviders } from '@angular/core';

import {
    API_BASE_URL,
} from './api.service';


// MORE INFO: https://github.com/RSuter/NSwag/wiki/SwaggerToTypeScriptClientGenerator%3A-Angular

@NgModule({
    providers: [
        { provide: API_BASE_URL, useValue: 'http://localhost:44321' }
    ]
})
export class ServicesModule {
    static forRoot(): ModuleWithProviders {
        return {
            ngModule: ServicesModule,
            providers: []
        };
    }
}
