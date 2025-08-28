import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FlagsService } from '../../services/flags.service';
import { FormsModule } from '@angular/forms';

@Component({
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  
})
export class HomeComponent implements OnInit {
  private flags = inject(FlagsService);
  items: any[] = [];
  loading = false;
  error = '';
  searchTerm = '';

  ngOnInit() {
    this.flags.getAllFlags().subscribe({
      next: (rows) => {
        this.items = (rows as any[])
          .filter(r => r?.countryName && (r?.flagUrl))
          .map(r => ({ countryName: r.countryName, flagUrl: r.flagUrl }))
          .sort((a, b) => a.countryName.localeCompare(b.countryName));
        this.loading = false;
      },
      error: () => { this.error = 'Failed to load flags'; this.loading = false; }
    });
  }

  get filteredItems() {
    if (!this.searchTerm) return this.items;
    const term = this.searchTerm.toLowerCase();
    return this.items.filter(c =>
      c.countryName.toLowerCase().includes(term)
    );
  }
}