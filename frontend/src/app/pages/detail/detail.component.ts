import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CountryApiService } from '../../services/country-api.service';

@Component({
  standalone: true,
  imports: [CommonModule, RouterLink],
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export class DetailComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private api = inject(CountryApiService);
  loading = true; error?: string; vm?: { countryName: string; population: number; capital?: string[]; flagUrl: string, regions: string };

  ngOnInit() {
    debugger;
    const name = this.route.snapshot.paramMap.get('name')!;
    this.api.getCountry(name).subscribe({
      next: (c) => { this.vm = c; this.loading = false; },
      error: () => { this.error = 'Country not found'; this.loading = false; }
    });
  }
}