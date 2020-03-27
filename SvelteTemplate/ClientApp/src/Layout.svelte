<script>
  import { getContext } from "svelte";
  import { user, reglament, toAsts, socket } from "./store.js";
  import { createEventDispatcher } from "svelte";
  import Modal from "./Shared/Modal.svelte";
  import { asRouterLink } from "./Shared/Actions.js";

  const dispatch = createEventDispatcher();
  let currentUser = $user;

  export let filter = null;

  let filterModal;
  
  let apply = () => {
    filterModal.hide();
    dispatch("apply", filter);
  };

  let showUser = false;
  let cispUrl = `${window.location.protocol}//${window.location.hostname}`;
  let loginUrl = `${window.location.protocol}//${window.location.hostname}/login?ReturnUrl=${window.location.href}`;

  let dev;
</script>

<style>
  .starter-template {
    padding: 0.5rem;
  }
</style>

<nav class="navbar navbar-expand-md navbar-light bg-light fixed-top">

  <a class="navbar-brand" href={cispUrl} title="КИСП">
    <img
      src="/favicon.png"
      width="30"
      height="30"
      class="d-inline-block align-top"
      alt="" />
  </a>

  <a class="navbar-brand" href="/">{getContext('name')}</a>

  <div class="collapse navbar-collapse" id="navbarsExampleDefault">

    <ul class="navbar-nav mr-auto">

      <li class="nav-item">
        <a use:asRouterLink class="nav-link" href="/about">About</a>
      </li>
      <li class="nav-item">
        <a use:asRouterLink={'active'} class="nav-link" href="/mols">
          Сотрудники
        </a>
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
          <slot name="actions">

            {#if $reglament}
              <a
                class="dropdown-item"
                href="/mols/new"
                class:disabled={!$reglament.allowEdit}>
                Новый сотрудник
              </a>
              <a
                class="dropdown-item"
                href="/mols/new"
                on:click|preventDefault={x => toAsts.message('Привет')}
                class:disabled={!$reglament.allowEdit}>
                Привет
              </a>
              <a
                class="dropdown-item"
                href="/mols/new"
                on:click|preventDefault={x => toAsts.error('И тебе привет')}
                class:disabled={!$reglament.allowEdit}>
                И тебе привет
              </a>
              <a
                class="dropdown-item"
                href="/mols/new"
                on:click|preventDefault={x => socket.send('Sent to socket')}
                class:disabled={!$reglament.allowEdit}>
                Sent to socket
              </a>
            {/if}

          </slot>
        </div>
      </li>
    </ul>

    {#if filter}
      <form class="form-inline" on:submit|preventDefault={apply}>
        <div class="input-group">
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
          <div class="input-group-append" id="button-addon4">
            <button
              class="btn btn-outline-secondary"
              type="button"
              on:click={x => filterModal.show()}>
              <i class="fa fa-filter" />
            </button>
          </div>
        </div>
      </form>
    {/if}

    <a href={loginUrl} class="ml-4">
      {#if $user && $user.name}
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

<Modal bind:modal={filterModal} class="modal-lg">
  <div slot="header" class="text-muted">
    <h5>
      <i class="fa fa-filter" />
      Фильтр
    </h5>
  </div>

  <form id="filterForm" on:submit|preventDefault={apply}>
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
