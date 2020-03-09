<script>
  import { getContext } from "svelte";
  import { user } from "../store.js";
  import { createEventDispatcher } from "svelte";
  import Modal from "./Modal.svelte";
  const dispatch = createEventDispatcher();

  export let filter = null;

  let name = getContext("name");



  let filterShow = false;
  let apply = () => {
    filterShow = false;
    dispatch("apply", filter);
  };

  let showUser = false;
  let loginUrl = `${window.location.protocol}//${window.location.hostname}/login?ReturnUrl=${window.location.href}`;

  let dev;
</script>

<style>
  .hidden {
    display: none;
  }

  .starter-template {
    padding: 0.5rem;
  }
</style>

<nav class="navbar navbar-expand-md navbar-light bg-light fixed-top">
  <a class="navbar-brand" href="/">{name}</a>
  <button
    class="navbar-toggler"
    type="button"
    data-toggle="collapse"
    data-target="/navbarsExampleDefault"
    aria-controls="navbarsExampleDefault"
    aria-expanded="false"
    aria-label="Toggle navigation">
    <span class="navbar-toggler-icon" />
  </button>

  <div class="collapse navbar-collapse" id="navbarsExampleDefault">

    <ul class="navbar-nav mr-auto">

      <li class="nav-item">
        <a class="nav-link" href="/about">About</a>
      </li>
      <li class="nav-item">
        <a class="nav-link" href="/mols">Пользователи</a>
      </li>

      <li class="nav-item dropdown">
        <a
          class="nav-link dropdown-toggle"
          href="/"
          data-toggle="dropdown"
          aria-haspopup="true"
          aria-expanded="false">
          Действия
        </a>
        <div class="dropdown-menu">
          <slot name="actions" />
        </div>
      </li>
    </ul>

    {#if filter}
      <form class="form-inline" on:submit|preventDefault={apply}>
        <div class="input-group">
          {#if filter.search !== undefined}
            <input
              class="form-control"
              type="search"
              placeholder="Поиск"
              bind:value={filter.search} />
            <div class="input-group-append" id="button-addon4">
              <button class="btn btn-outline-secondary" type="search">
                <i class="fa fa-search" />
              </button>
            </div>
          {/if}
          <div class="input-group-append" id="button-addon4">
            <button
              class="btn btn-outline-secondary"
              type="button"
              on:click={x => (filterShow = true)}>
              <i class="fa fa-filter" />
            </button>
          </div>

        </div>

      </form>
    {/if}

    <a href={loginUrl} class="ml-4">
      {#if $user.name}
        <img
          src="https://cisp.ssnab.ru/Files/MolsPhotos/w80/{$user.mol_id}.jpg"
          alt={$user.name}
          width="30"
          height="38"
          title={$user.name} />
      {:else}
        <i class="text-muted fa fa-question-circle fa-2x" />
      {/if}
    </a>
  </div>

</nav>

<main role="main">

  <div class="starter-template">
    <slot />
  </div>

</main>

<Modal bind:show={filterShow}>
  <div slot="header" class="text-muted">
    <h5>
      <i class="fa fa-filter" />
      Фильтр
    </h5>
  </div>
  <form id="filterForm" on:submit|preventDefault={apply} on:reset|preventDefault={apply}>
    <slot name="filter" {filter} />
  </form>

  <div slot="footer">
    <button type="submit" class="btn btn-outline-primary" form="filterForm">
      Применить
    </button>
    <button class="btn btn-outline-secondary" type="reset" form="filterForm">
      Очистить
    </button>

  </div>

</Modal>
